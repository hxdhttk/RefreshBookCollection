open System.IO
open System.Text.RegularExpressions

let booksSrc = @""

let booksDest = @""

let bookPathRegex = "\[(.+)\].+\.cbz" |> Regex

let parseBookPath bookPath =
    let matches = bookPathRegex.Match(bookPath)
    let author = matches.Groups.[1].Value
    author, bookPath

let sortBooks () =
    let books = Directory.EnumerateFiles(booksSrc, "*.cbz")
    books
    |> Seq.map parseBookPath
    |> Seq.groupBy fst
    |> Seq.map (fun x -> fst x, (snd x) |> Seq.map snd)
    |> dict

let run () = 
    let sorted = sortBooks()
    sorted
    |> Seq.iter (fun kv ->
        let author = kv.Key
        let books = kv.Value
        let newDir = Directory.CreateDirectory(Path.Combine(booksDest, author))
        books
        |> Seq.iter (fun book ->
            let bookFileInfo = book |> FileInfo
            let bookNewPath = Path.Combine(newDir.FullName, bookFileInfo.Name)
            bookFileInfo.MoveTo(bookNewPath))
        )

run ()
    