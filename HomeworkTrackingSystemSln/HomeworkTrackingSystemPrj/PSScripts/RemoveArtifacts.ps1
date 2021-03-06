cls
$siteUrl = "http://sp2010/HTS"
$Lists = @("Classes","ClassGrades", "HomeworkAssignments", "Students", "Submissions")
$ContentTypeGroupname = "Homework Tracker System - Content Types"
$SiteColumnGroupName = "Homework Tracker System - Site Columns"
Write-Host "Removing Lists"

$spWeb = Get-SPWeb $siteUrl
$ContentTypes = $spWeb.ContentTypes
Write-Host "------------------------------------------------------------------------------"
Write-Host "**** REMOVING CUSTOM CONTENT TYPES FROM ALL LISTS WITHIN THE CURRENT SITE ****" -Foreground Yellow
Write-Host "------------------------------------------------------------------------------"
foreach($List in $Lists)
{
    $spList = $spWeb.Lists.TryGetList($List)
    if($spList -ne $null)
    {
        foreach($ct in $ContentTypes)
        {
            $ctName = $ct.Name
            if(($spList.ContentTypes | Where{$_.Name -eq $ctName}) -ne $null)
            {
                $ctToRemove = $spList.ContentTypes[$ct.Name]
                Write-Host "Removing content type $($ctName) from list $($spList.Title)" -Foreground Yellow
                $spList.ContentTypes.Delete($ctToRemove.Id)
                $spList.Update()
                Write-Host "$($ctName) has been removed from $($spList.Title)" -Foreground Green
            } 
        }
    }
}

Write-Host "------------------------------------------------------------------------------------------"
Write-Host "**** ALL CUSTOM CONTENT TYPES HAS BEEN REMOVED FROM All LISTS WITHIN THE CURRENT SITE ****" -Foreground green
Write-Host "------------------------------------------------------------------------------------------"
Write-Host ""
Write-Host "---------------------------------------------------------"
Write-Host "**** REMOVING ALL CUSTOM LISTS FROM THE CURRENT SITE ****" -Foreground Yellow
Write-Host "---------------------------------------------------------"
foreach($List in $Lists)
{
    $spList = $spWeb.Lists.TryGetList($List)
    if($spList -ne $null)
    {
        Write-Host "Deleting list $($List)" -Foreground Yellow
        $spList.Delete()
        Write-host "$($List) has been deleted" -Foreground Green
    }
}
Write-Host "-----------------------------------------------------------------"
write-Host "**** ALL CUSTOM LISTS HAS BEEN REMOVED FROM THE CURRENT SITE ****" -Foreground Green
Write-Host "-----------------------------------------------------------------"
Write-Host ""
Write-Host "----------------------------------------------------------"
Write-Host "**** REMOVING ALL CONTENT TYPES FROM THE CURRENT SITE ****" -Foreground green
Write-Host "----------------------------------------------------------"
$ct = $spWeb.ContentTypes["Class"]
if($ct -ne $null)
{
    Write-Host "Deleting $($ct.Name) content type from the current site" -Foreground Yellow
    $ct.Delete()
    Write-Host "$($ct.Name) has been deleted." -Foreground green
}
$ct = $spWeb.ContentTypes["ClassGrades"]
if($ct -ne $null)
{
    Write-Host "Deleting $($ct.Name) content type from the current site" -Foreground Yellow
    $ct.Delete()
    Write-Host "$($ct.Name) has been deleted." -Foreground green
}
$ct = $spWeb.ContentTypes["Submissions"]
if($ct -ne $null)
{
    Write-Host "Deleting $($ct.Name) content type from the current site" -Foreground Yellow
    $ct.Delete()
    Write-Host "$($ct.Name) has been deleted." -Foreground Yellow
}
$ct = $spWeb.ContentTypes["HomeworkAssignments"]
if($ct -ne $null)
{
    Write-Host "Deleting $($ct.Name) content type from the current site" -Foreground Yellow
    $ct.Delete()
    Write-Host "$($ct.Name) has been deleted." -Foreground green
}
$ct = $spWeb.ContentTypes["Student"]
if($ct -ne $null)
{
    Write-Host "Deleting $($ct.Name) content type from the current site" -Foreground Yellow
    $ct.Delete()
    Write-Host "$($ct.Name) has been deleted." -Foreground green
}
Write-Host "------------------------------------------------------------------"
Write-Host "**** ALL CUSTOM CONTENT TYPES HAS BEEN REMOVED FROM THIS SITE ****" -Foreground green
Write-Host "------------------------------------------------------------------"

Write-Host "----------------------------------------------------------------"
write-host "**** DELETING ALL CUSTOM SITE COLUMNS FROM THE CURRENT SITE ****" -Foreground Yellow
Write-Host "----------------------------------------------------------------"
$SiteCols = $spWeb.Fields | Where {$_.Group -eq $SiteColumnGroupName}
if($SiteCols -ne $null)
{
    foreach($SiteCol in $SiteCols)
    {
        $ColTitle = $SiteCol.Title
        Write-Host "Removing $($ColTitle) column from current site" -Foreground Yellow
        $Col = $spWeb.Fields[$SiteCol.Title]
        $spWeb.Fields.Delete($Col)
        Write-Host "$($ColTitle) column has been removed from current site" -Foreground Green
    }
}
Write-Host "------------------------------------------------------------------------"
Write-Host "**** ALL CUSTOM SITE COLUMNS HAS BEEN REMOVED FROM THE CURRENT SITE ****" -Foreground Green
Write-Host "------------------------------------------------------------------------"
Write-Host ""
$spWeb.Dispose()
Write-Host "Done"-Foreground Green