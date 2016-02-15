// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake

RestorePackages()

// Build Directories
let buildDir = "./build/"
let testDir  = "./test/"

// Targets
Target "Clean" (fun _ -> 
  CleanDir buildDir
  CleanDir testDir
)

Target "BuildApp" (fun _ ->
  !! "src/app/**/*.fsproj
  |> MSBuildRelease buildDir "Build"
  |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ ->
  !! "src/test/**/*.fsproj
  |> MSBuildRelease testDir "Build"
  |> Log "TestBuild-Output: "
)

Target "Test" (fun _ ->
  !! (testDir + "/*Tests.dll")
  |> NUnitParallel (fun p ->
    {p with
      DisableShadowCopy = true;
    }
  )
)

Target "Default" (fun _ -> 
  trace "Building..."
)

"Clean"
  ===> "BuildApp"
  ===> "BuildTest"
  ===> "Test"
  ===> "Default"

RunTargetOrDefault "Default"
