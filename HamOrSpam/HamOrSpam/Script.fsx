#load "NaiveBayes.fs"
open NaiveBayes.Classifier
Hello "10"
open System.IO

type DocType = 
    | Ham
    | Spam

let parseDocType (label:string) = 
    match label with
    | "ham" -> Ham
    | "spam" -> Spam
    | _ -> failwith "Unknown lable"

let parseLine (line:string) = 
    let split = line.Split('\t')
    let label = split.[0] |> parseDocType
    let message = split.[1]
    (label, message)

let fileName = "SMSSpamCollection"
let path = __SOURCE_DIRECTORY__ + @"..\..\Data\" + fileName

let dataset = 
    File.ReadAllLines path
    |> Array.map parseLine

let spamWithFREE = 
    dataset
    |> Array.filter (fun (docType,_) -> docType = Spam)
    |> Array.filter (fun (_,sms) -> sms.Contains("FREE"))
    |> Array.length

let hamWithFREE = 
    dataset
    |> Array.filter (fun (docType,_) -> docType = Ham)
    |> Array.filter (fun (_,sms) -> sms.Contains("FREE"))
    |> Array.length

let primitiveClassifier (sms:string) =
    if (sms.Contains "FREE")
    then Spam
    else Ham  

