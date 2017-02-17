#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.WebAudio")
        .VersionFrom("WebSharper")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun fw -> fw.Net40)


let main =
    bt.WebSharper.Extension("IntelliFactory.WebSharper.WebAudio", directory = "WebAudio")
        .SourcesFromProject("WebAudio.fsproj")
        .References(fun r ->
            [
                r.NuGet("WebSharper.WebRTC").ForceFoundVersion().Reference()
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
                Title = Some "WebSharper.WebAudio"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.org/intellifactory/websharper.webaudio"
                Description = "WebSharper Extensions for WebAudio"
                Authors = ["IntelliFactory"]
                RequiresLicenseAcceptance = true })
        .Add(main)

]
|> bt.Dispatch
