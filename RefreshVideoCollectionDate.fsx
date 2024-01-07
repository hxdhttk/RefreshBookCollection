open System.IO

let videosDir = @""

let getLatestFile shelf =
    Directory.EnumerateFiles(shelf)
    |> Seq.map FileInfo
    |> Seq.maxBy (fun x -> x.LastWriteTime)

let refreshShelfDate shelf =
    let latestFile = getLatestFile shelf
    let shelfInfo = shelf |> DirectoryInfo
    shelfInfo.LastWriteTime <- latestFile.LastWriteTime

let run () =
    let shelves = Directory.EnumerateDirectories(videosDir)
    shelves |> Seq.iter refreshShelfDate

run ()
