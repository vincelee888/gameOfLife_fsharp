module Board

open Cell
open System

let ParseChar char = 
    match char with
    | 'x' -> DeadCell
    | _ -> LiveCell

let Parse (board:string) = 
    board.ToCharArray()
    |> Seq.map ParseChar
    |> Seq.toList

let Parse2d (board:string) =
    board.Split([|'\n'|])
    |> Seq.map Parse
    |> Seq.toList

let ParseCell cell =
    match cell with
    | LiveCell -> 'o'
    | _ -> 'x'

let ParseCells (cells:List<Cell>) =
    cells
    |> List.map ParseCell
    |> Array.ofList
    |> String

let Parse2dCells (board:List<List<Cell>>) =
    let lists = List.map ParseCells board
    String.concat "\n" lists

let NotWithinBounds (board, coords) =
    let x = fst coords
    let y = snd coords
    x < 0 || x > (List.length (List.head board)) - 1 ||  y < 0 || y > (List.length board) - 1

let GetCellAt (board:List<List<_>>, coords) =
    let x = fst coords
    let y = snd coords
    board.[y].[x]

let GetCell (board, coords) =
    match coords with
    | _ when NotWithinBounds (board, coords) -> None
    | _ -> Some(GetCellAt (board, coords))

let CountLiveNeighbour (cell:Option<Cell>):int =
    match cell with
    | Some(LiveCell) -> 1
    | _ -> 0

let GetNeighbours (board, coords) =
    let x = fst coords
    let y = snd coords
    let neighbours = [(x-1,y-1); (x,y-1); (x+1,y-1);
                      (x-1,y);            (x+1,y);
                      (x-1,y+1); (x,y+1); (x+1,y+1)]
    List.fold (fun acc neighbour -> acc + (CountLiveNeighbour (GetCell (board, neighbour)))) 0 neighbours