namespace Mahamudra.Cryptography.Notation   

open System
module Extensions =

    let ConcatArrays<'T> (arr1: 'T[]) (arr2: 'T[]) = 
        Array.concat [| arr1 ; arr2 |]
 