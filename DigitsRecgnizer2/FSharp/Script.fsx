open System.IO
open System.Security.Cryptography.X509Certificates

type Observation = { Label:string; Pixels: int[]}
type Distance = int[] * int[] -> int
let toObservation (csvData:string) =
    let columns = csvData.Split(',')
    let label = columns.[0]
    let pixels = columns.[1..] |> Array.map int
    { Label = label; Pixels = pixels }

let reader path =
    let data = File.ReadAllLines path
    data.[1..] |> Array.map toObservation

let trainingPath = @"E:\personal\deepLearning\DigitsRecognizer\data\trainingsample.csv"
let trainingData = reader trainingPath
let manhattanDistance (pixels1,pixels2) = 
    Array.zip pixels1 pixels2
    |> Array.map (fun (x,y) -> abs (x-y))
    |> Array.sum
let euclideanDistance (X, Y) = 
    Array.zip X Y
    |> Array.map(fun (x,y) -> pown (x-y) 2)
    |>Array.sum
let train (traningset:Observation[]) (dist:Distance) = 
    let classify (pixels:int[]) = 
        traningset
        |> Array.minBy (fun x -> dist (x.Pixels,pixels))
        |> fun x -> x.Label
    classify
let classifier = train trainingData euclideanDistance

let validationPath = @"E:\personal\deepLearning\DigitsRecognizer\data\validationsample.csv"
let validationData = reader validationPath

validationData
|> Array.averageBy (fun x -> if classifier x.Pixels = x.Label then 1. else 0.)
|> printfn "Correct: %.3f"