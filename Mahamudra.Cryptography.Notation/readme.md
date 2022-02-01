﻿# Mahamudra.Cryptography.Notation
![alt text](tezos-WN5_7UBc7cw-unsplash.jpg "Mahamudra Cryptography Notation")
[Photo by Tezos on Unsplash]

## EncodeBase58

##  What is Base58 encoding?
Base58 is a binary-to-text encoding created by Satoshi Nakamoto for Bitcoin addresses. 
The original Bitcoin client source code explains the reasoning behind base58 encoding:

base58.h:

// Why base-58 instead of standard base-64 encoding?
// - Don't want 0OIl characters that look the same in some fonts and
//      could be used to create visually identical looking account numbers.
// - A string with non-alphanumeric characters is not as easily accepted as an account number.
// - E-mail usually won't line-break if there's no punctuation to break at.
// - Doubleclicking selects the whole number as one word if it's all alphanumeric.


A simple package that implements the base-58 encoding.

## Usage

```F#
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



    [<TestMethod>]
    member this.EncodePlain_ShouldGiveString1_True () =
        let value = [|0x00uy; 0x00uy; 0x00uy; 0x01uy |]
        let string1 = "1112";
        let base58 = new Base58()
        let encoding = base58.EncodePlain(value); 
        let encodingv2 = base58.EncodePlainv2(value); 
        Assert.AreEqual(encoding, string1)
        Assert.AreEqual(encodingv2, string1)
```