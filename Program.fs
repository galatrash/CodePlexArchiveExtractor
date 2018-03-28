open System
open System.Diagnostics
open System.IO
open System.Reflection
open FSharp.Data

type ReleasesSchema = JsonProvider< "releaseList.json" >

let exeLocation =
    let p = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
    DirectoryInfo(p).FullName

let archivesFolder =
    let p = Path.Combine(exeLocation, @"..\..\..\CodePlexReleases")
    DirectoryInfo(p).FullName

let targetFolder =
    let p = Path.Combine(exeLocation, @"..\..\..\ExtractedReleases")
    DirectoryInfo(p).FullName

let loadAllReleases() = 
    let p = Path.Combine(archivesFolder, "releaseList.json")
    let name = FileInfo(p).FullName
    let text = File.ReadAllText name
    ReleasesSchema.Parse text

let promptExit() =
    if Debugger.IsAttached then 
        printf "\nPress [Enter] key to exit ... "
        Console.ReadLine() |> ignore

[<EntryPoint>]
let main argv = 
    try
        for release in loadAllReleases() do
            printfn "Processing Release %s" release.Name
            let destDinfo = Path.Combine(targetFolder, release.Name)
            if not (Directory.Exists(destDinfo)) then Directory.CreateDirectory(destDinfo) |> ignore
            for releaseFile in release.Files do
                let srcFile = Path.Combine(archivesFolder, releaseFile.Url.Replace('/', '\\'))
                let srcFinfo = new FileInfo(srcFile)
                let destFile = Path.Combine(destDinfo, releaseFile.FileName)
                if srcFinfo.Exists then
                    if not(File.Exists(destFile)) then
                        printfn "\tMoving %A to %s" srcFinfo.Name destFile
                        File.Move(srcFinfo.FullName, destFile)
                    else
                        printfn "\tDuplicate %A to %s" srcFinfo.FullName destFile
                else
                    printfn "\tCan't find source file: %s" srcFinfo.FullName
        promptExit()
        0
    with ex ->
        printfn "%A" ex
        promptExit()
        1
