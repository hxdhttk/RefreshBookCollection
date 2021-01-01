open System.IO

let booksDir = @""

let getLatestBook shelf =
    Directory.EnumerateFiles(shelf, "*.cbz")
    |> Seq.map FileInfo
    |> Seq.maxBy (fun x -> x.LastWriteTime)

let refreshShelfDate shelf =
    let latestBook = getLatestBook shelf
    let shelfInfo = shelf |> DirectoryInfo
    shelfInfo.LastWriteTime <- latestBook.LastWriteTime

let run () =
    let shelves = Directory.EnumerateDirectories(booksDir)
    shelves
    |> Seq.iter refreshShelfDate

run ()