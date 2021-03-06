Import-Module ActiveDirectory
Import-CSV "C:\HomeworkTrackerScripts\ADUsers.csv" | ForEach-Object{
    $User = Get-ADUser -LDAPFilter "(sAMAccountName=$($_."samAccountName"))"
    if($User -eq $Null)
    {
        Write-Host "Creating User $($_.Name)" -Foreground Yellow
        New-ADUser -Name $_.Name -Path $_."ParentOU" -SamAccountName $_."samAccountName" -UserPrincipalName "$($_."samAccountName")@SPDOM.Local" -DisplayName $_.Name -AccountPassword (ConvertTo-SecureString "Password1234" -AsPlainText -Force) -ChangePasswordAtLogon $false -Enable $true
        Write-Host "User $($_.Name) has been created" -Foreground Green
    }
    else
    {
        Write-Host "$($_.Name) user already exists." -Foreground Yellow
    }
}