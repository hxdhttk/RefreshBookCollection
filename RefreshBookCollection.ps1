Push-Location $PSScriptRoot

$booksSrc = ""

$updated = $false
[array]$newBooks = Get-ChildItem $booksSrc -Filter "*.cbz"
if ($newBooks.Length -gt 0) {
    Write-Host "New books:"
    foreach ($newBook in $newBooks) {
        Write-Host "    $($newBook.Name)"
    }

    dotnet fsi .\BooksSorter.fsx
    Write-Host "Moved new books to the book collection."
    $updated = $true
}

if ($updated) {
    dotnet fsi .\RefreshDate.fsx
    Write-Host "Refreshed the LastWriteTime."
}
else {
    Write-Host "No new books."
}

Pop-Location

Read-Host