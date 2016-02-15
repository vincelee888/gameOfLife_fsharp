module CellTests

open NUnit.Framework
open Swensen.Unquote
open Cell

[<Test>]
let ``Live Cell with less than 2 neighbours dies through under-population`` () =
    let result = NewState(LiveCell, 1)
    test <@ result = DeadCell @>

[<Test>]
let ``Live Cell with 2 neighbours lives`` () =
    let result = NewState(LiveCell, 2)
    test <@ result = LiveCell @>

[<Test>]
let ``Live Cell with 3 neighbours lives`` () =
    let result = NewState(LiveCell, 3)
    test <@ result = LiveCell @>

[<Test>]
let ``Live Cell with more than 3 neighbours dies through over-population`` () =
    let result = NewState(LiveCell, 4)
    test <@ result = DeadCell @>

[<Test>]
let ``Dead Cell remains dead`` () =
    let result = NewState(DeadCell, 0)
    test <@ result = DeadCell @>

[<Test>]
let ``Dead Cell with 3 neighbours lives`` () =
    let result = NewState(DeadCell, 3)
    test <@ result = LiveCell @>

[<Test>]
let ``Dead Cell with 2 neighbours remains dead`` () =
    let result = NewState(DeadCell, 2)
    test <@ result = DeadCell @>