// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake

// Build Directories
let buildDir = "./build/"
let testDir  = "./test/"

// Targets
Target "Clean" (fun _ -> 
  CleanDirs [buildDir; testDir]
)

Target "BuildApp" (fun _ ->
  !! "src/app/**/*.fsproj"
  |> MSBuildRelease buildDir "Build"
  |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ ->
  !! "src/test/**/*.fsproj"
  |> MSBuildDebug testDir "Build"
  |> Log "TestBuild-Output: "
)

Target "Test" (fun _ ->
  !! (testDir + "/*.Tests.dll")
  |> NUnit (fun p ->
    {p with
      DisableShadowCopy = true;
      OutputFile = testDir + "TestResults.xml"
    }
  )
)

Target "Default" (fun _ -> 
  trace "Building..."
)

"Clean"
  ==> "BuildApp"
  ==> "BuildTest"
  ==> "Test"
  ==> "Default"

RunTargetOrDefault "Default"
