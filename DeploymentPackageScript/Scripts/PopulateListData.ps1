$webUrl = "http://[SharePointSite]"
$ListName = "WorkRequests"

$spWeb = Get-SPWeb $webUrl
Write-Host "Starting PopulateListData script" -ForegroundColor Yellow
$spList = $spWeb.Lists.TryGetList($ListName)
if($spList -ne $null)
{
    $spListItem = $spList.items.add()
    $spListItem["Title"] = "Test List Item"
    $spListItem.Update();   
}
$spWeb.dispose()
Write-Host "Done" -ForegroundColor Green
