namespace NaiveBayes

module Classifier = 
    let Hello name = printfn "Hello, %s" name
    type Token = string
    type Tokenizer = string -> Token Set
    type TokenizedDoc = Token Set
    type DocsGroup = 
        { Proportion:float;
          TokenFrequencies:Map<Token,float>
        }
    let tokenScore (group:DocsGroup) (token:Token) = 
        if group.TokenFrequencies.ContainsKey token
        then log group.TokenFrequencies.[token]
        else 0.0
    let score (document:TokenizedDoc) (group:DocsGroup) = 
        let scoreToken = tokenScore group
        log group.Proportion + (document |> Seq.sumBy scoreToken)
    let classify (groups:(_*DocsGroup)[])
                 (tokenizer:Tokenizer)
                 (txt:string) =
        let tokenized = tokenizer txt
        groups
        |> Array.maxBy (fun (label,group) -> 
            score tokenized group)
        |> fst
