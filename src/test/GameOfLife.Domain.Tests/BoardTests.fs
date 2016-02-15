module BoardTests

open NUnit.Framework
open Swensen.Unquote
open Board
open Cell

[<Test>]
let ``o represents a live cell`` () =
    let result = ParseChar 'o'
    test <@ result = LiveCell @>

[<Test>]
let ``live cell is represented by o`` () =
    let result = ParseCell LiveCell
    test <@ result = 'o' @>

[<Test>]
let ``dead cell is represented by x`` () =
    let result = ParseCell DeadCell
    test <@ result = 'x' @>

[<Test>]
let ``x represents a dead cell`` () =
    let result = ParseChar 'x'
    test <@ result = DeadCell @>

[<Test>]
let ``string to char list`` () =
    let result = "abc".ToCharArray()
    let myList = result |> Seq.toList
    test <@ myList = ['a'; 'b'; 'c'] @>

[<Test>]
let ``parse row`` () =
    let result = Parse "xox"
    test <@ result = [DeadCell; LiveCell; DeadCell] @>

[<Test>]
let ``parse row of cell`` () =
    let result = ParseCells [DeadCell; LiveCell; DeadCell]
    test <@ result = "xox" @>

[<Test>]
let ``parse two dimensional`` () =
    let result = Parse2d "xo\nox"
    test <@ result = [[DeadCell; LiveCell]; [LiveCell; DeadCell]] @>

[<Test>]
let ``parse two dimensional lists of cells`` () =
    let result = Parse2dCells [[DeadCell; LiveCell]; [LiveCell; DeadCell]]
    test <@ result = "xo\nox" @>

[<Test>]
let ``get cell at valid coordinate returns Some Cell`` () =
    let start = [[LiveCell]]
    let result = GetCell(start, (0, 0))
    test <@ result = Some(LiveCell) @>

[<Test>]
let ``get cell in single row at valid coordinate returns Some Cell`` () =
    let start = [[LiveCell; DeadCell]]
    let result = GetCell(start, (1, 0))
    test <@ result = Some(DeadCell) @>
    
[<Test>]
let ``x coordinate less than 0 returns None`` () =
    let start = [[LiveCell]]
    let result = GetCell(start, (-1, 0))
    test <@ result = None @>

[<Test>]
let ``x coordinate greater than width returns None`` () =
    let start = [[LiveCell]]
    let result = GetCell(start, (1, 0))
    test <@ result = None @>

[<Test>]
let ``y coordinate less than 0 returns None`` () =
    let start = [[LiveCell]]
    let result = GetCell(start, (0, -1))
    test <@ result = None @>

[<Test>]
let ``y coordinate greater than width returns None`` () =
    let start = [[LiveCell]]
    let result = GetCell(start, (0, 1))
    test <@ result = None @>

[<Test>]
let ``within bounds`` () =
    let result = NotWithinBounds ([[LiveCell]], (0,0))
    test <@ result = false @>

[<Test>]
let ``request coord horizontally out of bounds`` () =
    let result = NotWithinBounds ([[LiveCell]], (1,0))
    test <@ result = true @>

[<Test>]
let ``request coord vertically out of bounds`` () =
    let result = NotWithinBounds ([[LiveCell]], (0,1))
    test <@ result = true @>