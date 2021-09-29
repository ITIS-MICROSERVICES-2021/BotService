// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open IronPython.Hosting
open Microsoft.Scripting.Hosting

let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#"
    printfn "Hello world %s" message
    
    let engine = Python.CreateEngine()
                         
    let paths = engine.GetSearchPaths()
    //paths.Add(@"C:\Python34\Lib")
    //paths.Add(@"C:\Python34\Lib\site-packages")
    paths.Add(@"C:\Python34\Lib")
    paths.Add(@"C:\Python34\Lib\site-packages")
    engine.SetSearchPaths(paths)
    
    engine.ExecuteFile("./main.py") |> ignore;
    0