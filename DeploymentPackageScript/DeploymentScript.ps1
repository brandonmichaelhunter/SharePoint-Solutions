#Script tasks
#1. Read an xml configuration file from it's current directory
#2. Identify which features need to be activated\deactivated, uninstall\install
#3. Create a log  file that log s the progress and errors produce by the script.
#4. Provide a debug option that shows the script output to console
cls


$xdoc = new-object System.Xml.XmlDocument

#Load deployment configuration file.
$workingDirectory = $PSScriptRoot
$solutionsFolderName = "Solutions\"
$configFilePath  = "Configuration\DeploymentConfig.xml"
$configDoc       = [xml] (get-content $workingDirectory""$configFilePath)
$solutionPath    = $workingDirectory+"\"+$solutionsFolderName
$logFilePath     = ""
#Get debug mode setting
$enableDebug = [boolean] $configDoc.DeploymentConfig.GetAttribute("DebugMode")

function CreateLogFile()
{
    $logFilePath = ('.\Logs\'+$configDoc.DeploymentConfig.GetAttribute("LogFileName")) 
    $logFileName = $configDoc.DeploymentConfig.GetAttribute("LogFileName")
    if((Test-Path $logFilePath) -eq $false)
    {
        $logFilePath = ".\Logs\$($logFileName)"
        Write-Host "Creating log file $($logFileName)"
        Add-Content -Path $logFilePath -Value "" -Force
        Write-Host "Log file created" 
    }
}

function log ($Msg, $color)
{
   if ($color -eq $null) {$color = "white"}
   if($enableDebug -eq $true)
   {
        Write-Host $Msg 
   }
   $Msg | out-file -Filepath $logFilePath -append
}
function Main()
{
    ###################
    # Variable Members
    ###################
    $logFilePath = resolve-path ('.\Logs\'+$configDoc.DeploymentConfig.GetAttribute("LogFileName"))
    log "---------------------------------------------------------"
    log "Script Execution Start Date and Time: $(get-date)"
    log "---------------------------------------------------------"
    ###################################
    # Recycle the IIS Application Pool#
    ###################################
    log "------------------------------" 
    log "Recycling the Application Pool" 
    log "------------------------------"
    log ""
    $appPoolName = [string] $configDoc.DeploymentConfig.GetAttribute("AppPoolName")
    $appPool = Get-WmiObject -Namespace root\MicrosoftIISv2 -Class IIsApplicationPool | Where-Object {$_.Name -eq "W3SVC/APPPOOLS/$appPoolName"}
    $appPool.Recycle()
    log "-------------------------" 
    log "Application pool recycled" 
    log "-------------------------" 

    $spSnapIn = Get-PSSnapin | Where-Object {$_.Name -eq 'Microsoft.SharePoint.Powershell'}
    if ($spSnapIn -eq $null){
       Add-PSSnapin Microsoft.SharePoint.Powershell
    }

    
    Start-SPAssignment -global 
 
    #Get web app url
    $webAppUrl = $configDoc.DeploymentConfig.Solutions.GetAttribute("WebAppUrl")

    #Gather information from each soluiton.
    $Solutions = $configDoc.DeploymentConfig.Solutions.Solution
    foreach($Solution in $Solutions)
    {
        $SolutionName = $Solution.Name
        $RequiresGACDeployment = [boolean] $Solution.GetAttribute("RequiresGACDeployment")
        ########################################
        # Deactivate and uninstall each feature#
        ########################################
        $Features = $Solution.Features.Feature
        #foreach($Feature in $Features){
            log "-------------------------------------------" 
            log "Deactivating and uninstall all features...." 
            log "-------------------------------------------" 
            foreach($Feature in $Features)
            {    
            
                if((Get-SPFeature -Limit All | Where-Object {$_.DisplayName -eq $Feature.Name}) -ne $null)
                {   
                   #Check to see if the feature is activated. 
                   $spFeature = Get-SPFeature -Limit All | Where-Object {$_.DisplayName -eq $Feature.Name}
                   if(Get-SPFeature -Limit All | Where{$_.ID -eq $spFeature.Id})
                   {
                        log "  Deactivating feature $($Feature.Name) " 
                        Disable-SPFeature -Identity $Feature.Name -Url $Feature.Url -Force -Confirm:$false
                        log "  $($Feature.Name) feature has been deactivated" 
                        log ""
                   }
                   else
                   {
                        log " $($Feature.Name) feature was not activated" 
                        log ""
                   }
                   log "  Uninstalling feature $($Feature.Name)" 
                   Uninstall-SPFeature -Identity $Feature.Name -Force -Confirm:$false
                   log "  $($Feature.Name) feature has been uninstalled" 
                   log ""
                }
            }
            log "--------------------------------------------------" 
            log "All Features has been deactivated and uninstall..." 
            log "--------------------------------------------------" 
            log ""
        
            ####################################
            #Uninstall and remove the solution.#
            ####################################
            $spSolution = Get-SPSolution | where-object {$_.Name -eq $SolutionName}
            if($spSolution -ne $null)
            {
                log "  Uninstalling  $($SolutionName) solution from the farm." 
                if ($spSolution.Deployed -eq $true)
                {
                    Uninstall-SPSolution -Identity $SolutionName -WebApplication $webAppUrl -Confirm:$false
                }

                while ((Get-SPSolution $SolutionName).JobExists){
                    log "   Uninstalling solution..." 
                    Start-Sleep -Second 3
                }
                log "  $($SolutionName)  was uninstalled from the farm"     
                log ""
                log "  Removing $($SolutionName) solution from the farm" 
                Remove-SPSolution -Identity $SolutionName -Confirm:$false
                log "  $($SolutionName) has been removed from the farm." 
                log ""
            }

            #######################
            #Restart Timer Service#
            #######################
            log "------------------------" 
            log "Restarting Timer Service" 
            log "------------------------" 
            Restart-Service SPTimerV4
            While ($(Get-Service SPTimerV4).Status -ne "Running")
            {
              log " Waiting for Timer Service to start" 
            }
            log "-------------------------" 
            log "Timer Services restarted." 
            log "-------------------------" 
            log ""

            ##########################################
            #Add and install the solution to the farm#
            ##########################################
        
            #Adding solution
            log "----------------------------------" 
            log "Installing and adding soluiton... " 
            log "----------------------------------" 
            log ""
            $SolutionPath = resolve-path ('Solutions\'+$SolutionName)
            log "  Adding $($SolutionName) solution to the farm" 
            $spSolution = Add-SPSolution -LiteralPath $SolutionPath
            log "  $($SolutionName) solution has been added to the farm." 
        
            #Install solution
            log "  Installing  $($SolutionName)  solution to the farm" 
            if($RequiresGACDeployment -eq $true){
                Install-SPSolution -Identity $SolutionName -WebApplication $webAppUrl -GACDeployment -Force
            }
            else{
                Install-SPSolution -Identity $SolutionName -WebApplication $webAppUrl -Force
            }

            $spSolution = Get-SPSolution -Identity $SolutionName
        
            if ($spSolution -ne $null)
            {
                While ($spSolution.JobExists)
                {
                    $jobStatus = $spSolution.JobStatus
   
                    # If the timer job succeeded then proceed
                    if ($jobStatus -eq [Microsoft.SharePoint.Administration.SPRunningJobStatus]::Succeeded)
                    {
                        log "  Solution $($SolutionName) timer job succeeded"
                        break
                    }
  
                    # If the timer job failed or was aborted then fail
                    if ($jobStatus -eq [Microsoft.SharePoint.Administration.SPRunningJobStatus]::Aborted -or $jobStatus -eq [Microsoft.SharePoint.Administration.SPRunningJobStatus]::Failed){
                        log "  Solution $($SolutionName) has timer job status $($jobStatus)."
                        break
                    }
    
                    log "   Installing solution..." 
                    Start-Sleep -Second 3
                }
             
                log "  $($SolutionName) has been installed." 
                log ""

                # Install and Activate the features
                log "------------------------------------" 
                log "Install and activate all features..." 
                log "------------------------------------" 
                log ""
                foreach($Feature in $Features)
                {
                    log "  Installing feature $($Feature.Name) " 
                    $spInstall = Install-SPFeature -Path $Feature.Name -Confirm:$false -Force
                    log "  $($Feature.Name) was installed" 
                    log " "
                    log "  Activiating Feature $($Feature.Name)" 
                    $spEnable  = Enable-SPFeature -Identity $Feature.Name -Url $Feature.Url -Force -Confirm:$false   
                    log "  $($Feature.Name) has been activated" 
                    log ""
                }
                log ""
                log "---------------------------------------------"  
                log "All features has been installed and activated"  
                log "---------------------------------------------" 
            }
            ###################
            # Execute scripts #
            ###################
            log ""
            log "---------------------------------------" 
            log "Executing additional powershell scripts" 
            log "---------------------------------------" 
            $Scripts = $Solutions.Scripts.Script
            foreach($Script in $Scripts)
            {
                $ScriptFileName = $Script.Name
                log "**** Executing script $($ScriptFileName)" 
                $ScriptFilePath  = resolve-path ('Scripts\'+$ScriptFileName)
                Invoke-Expression $ScriptFilePath
                log "**** $($ScriptFileName) executed" 
            }
            log ""
            log "-----------------------------------------" 
            log "All PowerShell scripts has been executed." 
            log "-----------------------------------------" 
    }

    Stop-SPAssignment -global
    log ""
    log "Done"
    log "---------------------------------------------------------" 
    log "Script Execution End Date and Time: $(get-date)"
    log "---------------------------------------------------------"
    log "***********************************************************************************************"
    log " "
}
CreateLogFile
Main
