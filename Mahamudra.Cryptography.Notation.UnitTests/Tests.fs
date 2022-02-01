namespace Mahamudra.Cryptography.Notation.UnitTests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Mahamudra.Cryptography.Notation

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.EncodePlain_ShouldGiveConsistentEncoding_True () =
        let value = [|0x00uy; 0x21uy; 0x60uy; 0x1Fuy;0xA1uy; 0x07uy; 0x20uy; 0x20uy;  0x20uy;  0x20uy; 0x20uy;  0x20uy; 0x20uy |]
        let base58 = new Base58()
        let encoding = base58.EncodePlain(value);
        let encodingv2 = base58.EncodePlainv2(value);
        Assert.AreEqual(encoding, encodingv2)


    [<TestMethod>]
    member this.EncodePlain_ShouldGiveString1_True () =
        let value = [|0x00uy; 0x00uy; 0x00uy; 0x01uy |]
        let string1 = "1112";
        let base58 = new Base58()
        let encoding = base58.EncodePlain(value); 
        let encodingv2 = base58.EncodePlainv2(value); 
        Assert.AreEqual(encoding, string1)
        Assert.AreEqual(encodingv2, string1)
 
    [<TestMethod>]
    member this.EncodePlain_ShouldGiveString2_True () =
        let value = [| 0x73uy;0x69uy;0x6duy;0x70uy;0x6cuy;0x79uy;0x20uy;0x61uy;0x20uy;0x6cuy;0x6fuy;0x6euy;0x67uy;0x20uy;0x73uy;0x74uy;0x72uy;0x69uy;0x6euy;0x67uy|]
        let string2 = "2cFupjhnEsSn59qHXstmK2ffpLv2";
        let base58 = new Base58()
        let encoding = base58.EncodePlain(value); 
        let encodingv2 = base58.EncodePlainv2(value); 
        Assert.AreEqual(encoding, string2)
        Assert.AreEqual(encodingv2, string2)

 
    
      