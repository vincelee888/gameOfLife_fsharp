module Cell

open System

type Cell = 
    | DeadCell
    | LiveCell
    
let NewState (cell, totalNeighbours) =
    match (cell, totalNeighbours) with
    | (LiveCell, 2) -> LiveCell
    | (_, 3) -> LiveCell
    | (_, _) -> DeadCell