module GameTests

open NUnit.Framework
open Swensen.Unquote
open Game
open Cell
open Board

[<Test>]
let ``single cell has no neighbours`` () =
    let board = [[LiveCell]]
    let result = GetNeighbours(board, (0,0))
    test <@ result = 0 @> 

[<Test>]
let ``two cells have one neighbour`` () =
    let board = [[LiveCell; LiveCell]]
    let result = GetNeighbours(board, (0,0))
    test <@ result = 1 @> 