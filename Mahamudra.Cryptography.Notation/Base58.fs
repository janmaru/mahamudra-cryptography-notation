namespace Mahamudra.Cryptography.Notation   

open System.Numerics 
open System.Security.Cryptography
open System

type Base58() = 
    let CheckSumSize:int =  4 
    let Digits:string = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz"
    let ArryDigits = ['1'..'9']@['A'..'H']@['J'..'N']@['P'..'Z']@['a'..'k']@['m'..'z'] |> List.toArray 

    member this.Encode (data:byte[]):string = 
         this.AddCheckSum(data) |>  this.EncodePlain  
    
    (** Encodes data in plain Base58, without any checksum. **)
    member this.EncodePlain (bdata:byte[]):string =
        (** Decode byte[] to BigInteger   **)
        let mutable intData = bdata |> Array.fold (fun acc b ->  acc*256I + bigint(int b)) 0I 
        let mutable result:string = ""  (**  Encode BigInteger to Base58 string  **)
        while intData > 0I  do
            let remainder = (int)(intData % 58I)
            intData <- intData /58I
            result <- (string)Digits[remainder] + result 
        (** Append `1` for each leading 0 byte **)
        for i in 0 .. bdata.Length-1 do
            if(bdata[i] = 0uy) then
                result <- "1" + result;
        result

    member this.EncodePlainv2 (bdata:byte[]):string =
        let data = bdata |> Array.toList
    
        let rec toBigInt = function
            |[], acc -> acc
            |h::t, acc -> toBigInt(t, acc*256I + bigint(int h)) 

        let rec base58encode  = function
            | i,acc when i>0I ->
                let reminder = ref 0I
                let dividend = bigint.DivRem(i, 58I, reminder)
                let char = ArryDigits.[(int)reminder.contents]
                base58encode(dividend, char::acc)
            | _,acc -> acc

        let appendOnes = 
            let rec insertOnes = function
                | h::t,acc when h=0uy -> insertOnes(t, '1'::acc)
                | _,acc -> acc
            insertOnes(data, [])

        let big = toBigInt(data, 0I) 
        let encoded = appendOnes @ base58encode(big, []) |> List.toArray
        encoded |> System.String 

    member this.CheckSum (bdata:byte[]):byte[] =
        use sha256:SHA256 = SHA256.Create()
        let hash1 = sha256.ComputeHash(bdata) 
        let hash2 = sha256.ComputeHash(hash1)  
        Array.copy hash2 

    member this.AddCheckSum (bdata:byte[]):byte[] =
            let checkSum = this.CheckSum bdata
            Extensions.ConcatArrays bdata checkSum
 