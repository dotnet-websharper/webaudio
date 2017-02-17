#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("Zafir.WebAudio")
        .VersionFrom("Zafir")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun fw -> fw.Net40)


let main =
    bt.Zafir.Extension("WebSharper.WebAudio", directory = "WebAudio")
        .SourcesFromProject("WebAudio.fsproj")
        .References(fun r ->
            [
                r.NuGet("Zafir.WebRTC").Latest(true).ForceFoundVersion().Reference()
            ])

(*let test =
    bt.WebSharper.BundleWebsite("IntelliFactory.WebSharper.WebAudio.Tests", directory = "Tests")
        .SourcesFromProject("Tests.fsproj")
        .References(fun r -> [r.Project main])*)

bt.Solution [
    main
    //test

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "Zafir.WebAudio"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.org/intellifactory/websharper.webaudio"
                Description = "WebSharper Extensions for WebAudio"
                Authors = ["IntelliFactory"]
                RequiresLicenseAcceptance = true })
        .Add(main)

]
|> bt.Dispatch
