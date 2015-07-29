// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  

namespace FearTheCowboy.MessagePack.Types {
    internal enum Format : byte {
        PositiveFixIntMin = 0x00,
        PositiveFixedInt0 = 0x00,
        PositiveFixedInt1,
        PositiveFixedInt2,
        PositiveFixedInt3,
        PositiveFixedInt4,
        PositiveFixedInt5,
        PositiveFixedInt6,
        PositiveFixedInt7,
        PositiveFixedInt8,
        PositiveFixedInt9,
        PositiveFixedInt10,
        PositiveFixedInt11,
        PositiveFixedInt12,
        PositiveFixedInt13,
        PositiveFixedInt14,
        PositiveFixedInt15,
        PositiveFixedInt16,
        PositiveFixedInt17,
        PositiveFixedInt18,
        PositiveFixedInt19,
        PositiveFixedInt20,
        PositiveFixedInt21,
        PositiveFixedInt22,
        PositiveFixedInt23,
        PositiveFixedInt24,
        PositiveFixedInt25,
        PositiveFixedInt26,
        PositiveFixedInt27,
        PositiveFixedInt28,
        PositiveFixedInt29,
        PositiveFixedInt30,
        PositiveFixedInt31,
        PositiveFixedInt32,
        PositiveFixedInt33,
        PositiveFixedInt34,
        PositiveFixedInt35,
        PositiveFixedInt36,
        PositiveFixedInt37,
        PositiveFixedInt38,
        PositiveFixedInt39,
        PositiveFixedInt40,
        PositiveFixedInt41,
        PositiveFixedInt42,
        PositiveFixedInt43,
        PositiveFixedInt44,
        PositiveFixedInt45,
        PositiveFixedInt46,
        PositiveFixedInt47,
        PositiveFixedInt48,
        PositiveFixedInt49,
        PositiveFixedInt50,
        PositiveFixedInt51,
        PositiveFixedInt52,
        PositiveFixedInt53,
        PositiveFixedInt54,
        PositiveFixedInt55,
        PositiveFixedInt56,
        PositiveFixedInt57,
        PositiveFixedInt58,
        PositiveFixedInt59,
        PositiveFixedInt60,
        PositiveFixedInt61,
        PositiveFixedInt62,
        PositiveFixedInt63,
        PositiveFixedInt64,
        PositiveFixedInt65,
        PositiveFixedInt66,
        PositiveFixedInt67,
        PositiveFixedInt68,
        PositiveFixedInt69,
        PositiveFixedInt70,
        PositiveFixedInt71,
        PositiveFixedInt72,
        PositiveFixedInt73,
        PositiveFixedInt74,
        PositiveFixedInt75,
        PositiveFixedInt76,
        PositiveFixedInt77,
        PositiveFixedInt78,
        PositiveFixedInt79,
        PositiveFixedInt80,
        PositiveFixedInt81,
        PositiveFixedInt82,
        PositiveFixedInt83,
        PositiveFixedInt84,
        PositiveFixedInt85,
        PositiveFixedInt86,
        PositiveFixedInt87,
        PositiveFixedInt88,
        PositiveFixedInt89,
        PositiveFixedInt90,
        PositiveFixedInt91,
        PositiveFixedInt92,
        PositiveFixedInt93,
        PositiveFixedInt94,
        PositiveFixedInt95,
        PositiveFixedInt96,
        PositiveFixedInt97,
        PositiveFixedInt98,
        PositiveFixedInt99,
        PositiveFixedInt100,
        PositiveFixedInt101,
        PositiveFixedInt102,
        PositiveFixedInt103,
        PositiveFixedInt104,
        PositiveFixedInt105,
        PositiveFixedInt106,
        PositiveFixedInt107,
        PositiveFixedInt108,
        PositiveFixedInt109,
        PositiveFixedInt110,
        PositiveFixedInt111,
        PositiveFixedInt112,
        PositiveFixedInt113,
        PositiveFixedInt114,
        PositiveFixedInt115,
        PositiveFixedInt116,
        PositiveFixedInt117,
        PositiveFixedInt118,
        PositiveFixedInt119,
        PositiveFixedInt120,
        PositiveFixedInt121,
        PositiveFixedInt122,
        PositiveFixedInt123,
        PositiveFixedInt124,
        PositiveFixedInt125,
        PositiveFixedInt126,
        PositiveFixedInt127,
        PositiveFixIntMax = 0x7f,
        FixMapMin = 0x80,
        FixMap0 = 0x80,
        FixMap1,
        FixMap2,
        FixMap3,
        FixMap4,
        FixMap5,
        FixMap6,
        FixMap7,
        FixMap8,
        FixMap9,
        FixMap10,
        FixMap11,
        FixMap12,
        FixMap13,
        FixMap14,
        FixMap15,

        FixMapMax = 0x8f,
        FixArrayMin = 0x90,
        FixArray0 = 0x90,
        FixArray1,
        FixArray2,
        FixArray3,
        FixArray4,
        FixArray5,
        FixArray6,
        FixArray7,
        FixArray8,
        FixArray9,
        FixArray10,
        FixArray11,
        FixArray12,
        FixArray13,
        FixArray14,
        FixArray15,
        FixArrayMax = 0x9f,
        FixStringMin = 0xa0,
        FixString0 = 0xa0,
        FixString1,
        FixString2,
        FixString3,
        FixString4,
        FixString5,
        FixString6,
        FixString7,
        FixString8,
        FixString9,
        FixString10,
        FixString11,
        FixString12,
        FixString13,
        FixString14,
        FixString15,
        FixString16,
        FixString17,
        FixString18,
        FixString19,
        FixString20,
        FixString21,
        FixString22,
        FixString23,
        FixString24,
        FixString25,
        FixString26,
        FixString27,
        FixString28,
        FixString29,
        FixString30,
        FixString31,
        FixStringMax = 0xbf,
        Nil = 0xc0,
        Unused = 0xc1,
        False = 0xc2,
        True = 0xc3,
        Binary8 = 0xc4,
        Binary16 = 0xc5,
        Binary32 = 0xc6,
        Extended8 = 0xc7,
        Extended16 = 0xc8,
        Extended32 = 0xc9,
        Float32 = 0xca,
        Float64 = 0xcb,
        UInt8 = 0xcc,
        UInt16 = 0xcd,
        UInt32 = 0xce,
        UInt64 = 0xcf,
        Int8 = 0xd0,
        Int16 = 0xd1,
        Int32 = 0xd2,
        Int64 = 0xd3,
        FixExtended1 = 0xd4,
        FixExtended2 = 0xd5,
        FixExtended4 = 0xd6,
        FixExtended8 = 0xd7,
        FixExtended16 = 0xd8,
        String8 = 0xd9,
        String16 = 0xda,
        String32 = 0xdb,
        Array16 = 0xdc,
        Array32 = 0xdd,
        Map16 = 0xde,
        Map32 = 0xdf,
        NegativeFixIntMin = 0xe0,
        NegativeFixInt0 = 0xe0,
        NegativeFixInt1,
        NegativeFixInt2,
        NegativeFixInt3,
        NegativeFixInt4,
        NegativeFixInt5,
        NegativeFixInt6,
        NegativeFixInt7,
        NegativeFixInt8,
        NegativeFixInt9,
        NegativeFixInt10,
        NegativeFixInt11,
        NegativeFixInt12,
        NegativeFixInt13,
        NegativeFixInt14,
        NegativeFixInt15,
        NegativeFixInt16,
        NegativeFixInt17,
        NegativeFixInt18,
        NegativeFixInt19,
        NegativeFixInt20,
        NegativeFixInt21,
        NegativeFixInt22,
        NegativeFixInt23,
        NegativeFixInt24,
        NegativeFixInt25,
        NegativeFixInt26,
        NegativeFixInt27,
        NegativeFixInt28,
        NegativeFixInt29,
        NegativeFixInt30,
        NegativeFixInt31,
        NegativeFixIntMax = 0xff
    }
}