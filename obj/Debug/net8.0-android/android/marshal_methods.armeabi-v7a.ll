; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [328 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [650 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 248
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 282
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 39485524, ; 6: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42639949, ; 7: System.Threading.Thread => 0x28aa24d => 145
	i32 66541672, ; 8: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 67008169, ; 9: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 323
	i32 68219467, ; 10: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 11: Microsoft.Maui.Graphics.dll => 0x44bb714 => 201
	i32 80882872, ; 12: MiviaMaui => 0x4d22cb8 => 0
	i32 82292897, ; 13: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 98325684, ; 14: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 185
	i32 101534019, ; 15: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 266
	i32 117431740, ; 16: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 120558881, ; 17: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 266
	i32 122350210, ; 18: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 134690465, ; 19: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 286
	i32 142721839, ; 20: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 21: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 22: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 23: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 222
	i32 176265551, ; 24: System.ServiceProcess => 0xa81994f => 132
	i32 182336117, ; 25: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 268
	i32 184328833, ; 26: System.ValueTuple.dll => 0xafca281 => 151
	i32 195452805, ; 27: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 320
	i32 199333315, ; 28: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 321
	i32 205061960, ; 29: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 30: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 220
	i32 220171995, ; 31: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 221958352, ; 32: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 184
	i32 230216969, ; 33: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 242
	i32 230752869, ; 34: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 35: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 36: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 37: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 38: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 225
	i32 276479776, ; 39: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 278686392, ; 40: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 244
	i32 280482487, ; 41: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 241
	i32 280992041, ; 42: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 292
	i32 291076382, ; 43: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 291275502, ; 44: Microsoft.Extensions.Http.dll => 0x115c82ee => 189
	i32 298918909, ; 45: System.Net.Ping.dll => 0x11d123fd => 69
	i32 317674968, ; 46: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 320
	i32 318968648, ; 47: Xamarin.AndroidX.Activity.dll => 0x13031348 => 211
	i32 321597661, ; 48: System.Numerics => 0x132b30dd => 83
	i32 336156722, ; 49: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 305
	i32 342366114, ; 50: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 243
	i32 347068432, ; 51: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 205
	i32 356389973, ; 52: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 304
	i32 360082299, ; 53: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 54: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 55: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 56: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 57: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 58: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 59: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 60: _Microsoft.Android.Resource.Designer => 0x17969339 => 324
	i32 403441872, ; 61: WindowsBase => 0x180c08d0 => 165
	i32 435591531, ; 62: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 316
	i32 441335492, ; 63: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 226
	i32 442565967, ; 64: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 65: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 239
	i32 451504562, ; 66: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 67: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 68: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 69: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 70: System.dll => 0x1bff388e => 164
	i32 476646585, ; 71: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 241
	i32 486930444, ; 72: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 254
	i32 498788369, ; 73: System.ObjectModel => 0x1dbae811 => 84
	i32 500358224, ; 74: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 303
	i32 503918385, ; 75: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 297
	i32 513247710, ; 76: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 195
	i32 526420162, ; 77: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527452488, ; 78: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 286
	i32 530272170, ; 79: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 539058512, ; 80: Microsoft.Extensions.Logging => 0x20216150 => 190
	i32 540030774, ; 81: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 82: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 83: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 549171840, ; 84: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 85: Jsr305Binding => 0x213954e7 => 279
	i32 569601784, ; 86: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 277
	i32 577335427, ; 87: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 592146354, ; 88: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 311
	i32 597488923, ; 89: CommunityToolkit.Maui => 0x239cf51b => 173
	i32 601371474, ; 90: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 91: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 92: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 627609679, ; 93: Xamarin.AndroidX.CustomView => 0x2568904f => 231
	i32 627931235, ; 94: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 309
	i32 639843206, ; 95: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 237
	i32 643868501, ; 96: System.Net => 0x2660a755 => 81
	i32 662205335, ; 97: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 98: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 273
	i32 666292255, ; 99: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 218
	i32 672442732, ; 100: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 101: System.Net.Security => 0x28bdabca => 73
	i32 688181140, ; 102: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 291
	i32 690569205, ; 103: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 104: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 288
	i32 693804605, ; 105: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 106: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 107: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 283
	i32 700358131, ; 108: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 706645707, ; 109: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 306
	i32 709557578, ; 110: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 294
	i32 720511267, ; 111: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 287
	i32 722857257, ; 112: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 731701662, ; 113: Microsoft.Extensions.Options.ConfigurationExtensions => 0x2b9ce19e => 194
	i32 735137430, ; 114: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 748832960, ; 115: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 203
	i32 752232764, ; 116: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 117: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 208
	i32 759454413, ; 118: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 119: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 120: System.IO.Compression => 0x2e394f87 => 46
	i32 777317022, ; 121: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 315
	i32 789151979, ; 122: Microsoft.Extensions.Options => 0x2f0980eb => 193
	i32 790371945, ; 123: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 232
	i32 804715423, ; 124: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 125: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 246
	i32 823281589, ; 126: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 127: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 128: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 129: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 130: Xamarin.AndroidX.Print => 0x3246f6cd => 259
	i32 873119928, ; 131: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 132: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 133: System.Net.Http.Json => 0x3463c971 => 63
	i32 904024072, ; 134: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 135: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 926902833, ; 136: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 318
	i32 928116545, ; 137: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 282
	i32 952186615, ; 138: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 956575887, ; 139: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 287
	i32 966729478, ; 140: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 280
	i32 967690846, ; 141: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 243
	i32 975236339, ; 142: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 143: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 144: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 145: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 992768348, ; 146: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 147: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 999186168, ; 148: Microsoft.Extensions.FileSystemGlobbing.dll => 0x3b8e5ef8 => 188
	i32 1001831731, ; 149: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 150: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 263
	i32 1019214401, ; 151: System.Drawing => 0x3cbffa41 => 36
	i32 1028951442, ; 152: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 183
	i32 1029334545, ; 153: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 293
	i32 1031528504, ; 154: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 281
	i32 1035644815, ; 155: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 216
	i32 1036536393, ; 156: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1044663988, ; 157: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1048992957, ; 158: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 185
	i32 1052210849, ; 159: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 250
	i32 1067306892, ; 160: GoogleGson => 0x3f9dcf8c => 175
	i32 1082857460, ; 161: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 162: Xamarin.Kotlin.StdLib => 0x409e66d8 => 284
	i32 1098259244, ; 163: System => 0x41761b2c => 164
	i32 1106973742, ; 164: Microsoft.Extensions.Configuration.FileExtensions.dll => 0x41fb142e => 179
	i32 1118262833, ; 165: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 306
	i32 1121599056, ; 166: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 249
	i32 1127624469, ; 167: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 192
	i32 1149092582, ; 168: Xamarin.AndroidX.Window => 0x447dc2e6 => 276
	i32 1168523401, ; 169: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 312
	i32 1170634674, ; 170: System.Web.dll => 0x45c677b2 => 153
	i32 1173126369, ; 171: Microsoft.Extensions.FileProviders.Abstractions.dll => 0x45ec7ce1 => 186
	i32 1175144683, ; 172: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 272
	i32 1178241025, ; 173: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 257
	i32 1203215381, ; 174: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 310
	i32 1204270330, ; 175: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 218
	i32 1208641965, ; 176: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1219128291, ; 177: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1234928153, ; 178: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 308
	i32 1243150071, ; 179: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 277
	i32 1253011324, ; 180: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 181: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 292
	i32 1264511973, ; 182: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 267
	i32 1267360935, ; 183: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 271
	i32 1273260888, ; 184: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 223
	i32 1275534314, ; 185: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 288
	i32 1278448581, ; 186: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 215
	i32 1292207520, ; 187: SQLitePCLRaw.core.dll => 0x4d0585a0 => 204
	i32 1293217323, ; 188: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 234
	i32 1309188875, ; 189: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1322716291, ; 190: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 276
	i32 1324164729, ; 191: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 192: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1364015309, ; 193: System.IO => 0x514d38cd => 57
	i32 1373134921, ; 194: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 322
	i32 1376866003, ; 195: Xamarin.AndroidX.SavedState => 0x52114ed3 => 263
	i32 1379779777, ; 196: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 197: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 198: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 227
	i32 1408764838, ; 199: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 200: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 201: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1430672901, ; 202: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 290
	i32 1434145427, ; 203: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 204: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 280
	i32 1439761251, ; 205: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1452070440, ; 206: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 207: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 208: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 209: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1461004990, ; 210: es\Microsoft.Maui.Controls.resources => 0x57152abe => 296
	i32 1461234159, ; 211: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 212: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 213: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 214: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 217
	i32 1470490898, ; 215: Microsoft.Extensions.Primitives => 0x57a5e912 => 195
	i32 1479771757, ; 216: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 217: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 218: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 219: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 264
	i32 1493001747, ; 220: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 300
	i32 1505131794, ; 221: Microsoft.Extensions.Http => 0x59b67d12 => 189
	i32 1514721132, ; 222: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 295
	i32 1521091094, ; 223: Microsoft.Extensions.FileSystemGlobbing => 0x5aaa0216 => 188
	i32 1536373174, ; 224: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 225: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 226: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 227: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1551623176, ; 228: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 315
	i32 1565862583, ; 229: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 230: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 231: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 232: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1582372066, ; 233: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 233
	i32 1592978981, ; 234: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1597949149, ; 235: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 281
	i32 1601112923, ; 236: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 237: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 238: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 239: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 253
	i32 1622358360, ; 240: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1624863272, ; 241: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 275
	i32 1632842087, ; 242: Microsoft.Extensions.Configuration.Json => 0x61533167 => 180
	i32 1634654947, ; 243: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 174
	i32 1635184631, ; 244: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 237
	i32 1636350590, ; 245: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 230
	i32 1639515021, ; 246: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 247: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 248: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 249: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 250: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 269
	i32 1658251792, ; 251: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 278
	i32 1670060433, ; 252: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 225
	i32 1675553242, ; 253: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 254: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 255: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 256: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1691477237, ; 257: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 258: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 259: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 285
	i32 1701541528, ; 260: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 261: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 205
	i32 1720223769, ; 262: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 246
	i32 1726116996, ; 263: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 264: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 265: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 221
	i32 1736233607, ; 266: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 313
	i32 1743415430, ; 267: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 291
	i32 1744735666, ; 268: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 269: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 270: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 271: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 272: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 273: System.Reflection.Extensions => 0x6942234e => 93
	i32 1766324549, ; 274: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 268
	i32 1767075704, ; 275: Microsoft.Extensions.Configuration.UserSecrets.dll => 0x69536f78 => 181
	i32 1770582343, ; 276: Microsoft.Extensions.Logging.dll => 0x6988f147 => 190
	i32 1776026572, ; 277: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 278: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 279: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1782862114, ; 280: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 307
	i32 1788241197, ; 281: Xamarin.AndroidX.Fragment => 0x6a96652d => 239
	i32 1793755602, ; 282: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 299
	i32 1808609942, ; 283: Xamarin.AndroidX.Loader => 0x6bcd3296 => 253
	i32 1813058853, ; 284: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 284
	i32 1813201214, ; 285: Xamarin.Google.Android.Material => 0x6c13413e => 278
	i32 1818569960, ; 286: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 258
	i32 1818787751, ; 287: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 288: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 289: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1828688058, ; 290: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 191
	i32 1842015223, ; 291: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 319
	i32 1847515442, ; 292: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 208
	i32 1853025655, ; 293: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 316
	i32 1858542181, ; 294: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 295: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1875935024, ; 296: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 298
	i32 1879696579, ; 297: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 298: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 219
	i32 1888955245, ; 299: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 300: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1898237753, ; 301: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 302: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1910275211, ; 303: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 304: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1956758971, ; 305: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1961813231, ; 306: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 265
	i32 1968388702, ; 307: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 176
	i32 1983156543, ; 308: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 285
	i32 1985761444, ; 309: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 210
	i32 2003115576, ; 310: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 295
	i32 2011961780, ; 311: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 312: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 250
	i32 2025202353, ; 313: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 290
	i32 2031763787, ; 314: Xamarin.Android.Glide => 0x791a414b => 207
	i32 2045470958, ; 315: System.Private.Xml => 0x79eb68ee => 88
	i32 2048278909, ; 316: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 178
	i32 2055257422, ; 317: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 245
	i32 2060060697, ; 318: System.Windows.dll => 0x7aca0819 => 154
	i32 2066184531, ; 319: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 294
	i32 2070888862, ; 320: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2072397586, ; 321: Microsoft.Extensions.FileProviders.Physical => 0x7b864712 => 187
	i32 2079903147, ; 322: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 323: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2103459038, ; 324: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 206
	i32 2127167465, ; 325: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 326: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 327: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 328: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 329: Microsoft.Maui => 0x80bd55ad => 199
	i32 2169148018, ; 330: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 302
	i32 2181898931, ; 331: Microsoft.Extensions.Options.dll => 0x820d22b3 => 193
	i32 2192057212, ; 332: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 191
	i32 2193016926, ; 333: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2201107256, ; 334: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 289
	i32 2201231467, ; 335: System.Net.Http => 0x8334206b => 64
	i32 2207618523, ; 336: it\Microsoft.Maui.Controls.resources => 0x839595db => 304
	i32 2217644978, ; 337: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 272
	i32 2222056684, ; 338: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2244775296, ; 339: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 254
	i32 2252106437, ; 340: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2256313426, ; 341: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 342: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2266799131, ; 343: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 177
	i32 2267999099, ; 344: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 209
	i32 2270573516, ; 345: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 298
	i32 2279755925, ; 346: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 261
	i32 2293034957, ; 347: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 348: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 349: System.Net.Mail => 0x88ffe49e => 66
	i32 2303942373, ; 350: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 308
	i32 2305521784, ; 351: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 352: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 213
	i32 2320631194, ; 353: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2340441535, ; 354: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 355: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 356: System.Net.Primitives => 0x8c40e0db => 70
	i32 2368005991, ; 357: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2371007202, ; 358: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 176
	i32 2378619854, ; 359: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 360: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2395872292, ; 361: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 303
	i32 2401565422, ; 362: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 363: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 236
	i32 2421380589, ; 364: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2423080555, ; 365: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 223
	i32 2427813419, ; 366: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 300
	i32 2435356389, ; 367: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 368: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 369: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 370: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 371: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465273461, ; 372: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 203
	i32 2465532216, ; 373: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 226
	i32 2471841756, ; 374: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 375: Java.Interop.dll => 0x93918882 => 168
	i32 2480646305, ; 376: Microsoft.Maui.Controls => 0x93dba8a1 => 197
	i32 2483903535, ; 377: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 378: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 379: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 380: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 381: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 248
	i32 2522472828, ; 382: Xamarin.Android.Glide.dll => 0x9659e17c => 207
	i32 2538310050, ; 383: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2550873716, ; 384: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 301
	i32 2562349572, ; 385: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 386: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2581783588, ; 387: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 249
	i32 2581819634, ; 388: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 271
	i32 2585220780, ; 389: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 390: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 391: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2592341985, ; 392: Microsoft.Extensions.FileProviders.Abstractions => 0x9a83ffe1 => 186
	i32 2593496499, ; 393: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 310
	i32 2605712449, ; 394: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 289
	i32 2615233544, ; 395: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 240
	i32 2616218305, ; 396: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 192
	i32 2617129537, ; 397: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 398: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 399: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 230
	i32 2624644809, ; 400: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 235
	i32 2626831493, ; 401: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 305
	i32 2627185994, ; 402: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 403: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 404: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 244
	i32 2663391936, ; 405: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 209
	i32 2663698177, ; 406: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 407: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 408: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 409: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 410: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 411: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 412: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 269
	i32 2715334215, ; 413: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 414: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 415: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 416: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 417: Xamarin.AndroidX.Activity => 0xa2e0939b => 211
	i32 2735172069, ; 418: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 419: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 217
	i32 2740948882, ; 420: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 421: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2752995522, ; 422: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 311
	i32 2758225723, ; 423: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 198
	i32 2764765095, ; 424: Microsoft.Maui.dll => 0xa4caf7a7 => 199
	i32 2765824710, ; 425: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 426: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 283
	i32 2778768386, ; 427: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 274
	i32 2779977773, ; 428: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 262
	i32 2785988530, ; 429: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 317
	i32 2788224221, ; 430: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 240
	i32 2801831435, ; 431: Microsoft.Maui.Graphics => 0xa7008e0b => 201
	i32 2803228030, ; 432: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2806116107, ; 433: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 296
	i32 2810250172, ; 434: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 227
	i32 2819470561, ; 435: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 436: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 437: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 262
	i32 2824502124, ; 438: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2831556043, ; 439: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 309
	i32 2838993487, ; 440: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 251
	i32 2849599387, ; 441: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 442: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 274
	i32 2855708567, ; 443: Xamarin.AndroidX.Transition => 0xaa36a797 => 270
	i32 2861098320, ; 444: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 445: Microsoft.Maui.Essentials => 0xaa8a4878 => 200
	i32 2868488919, ; 446: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 174
	i32 2870099610, ; 447: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 212
	i32 2875164099, ; 448: Jsr305Binding.dll => 0xab5f85c3 => 279
	i32 2875220617, ; 449: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2884993177, ; 450: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 238
	i32 2887636118, ; 451: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 452: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 453: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 454: System.Reflection => 0xacf080de => 97
	i32 2905242038, ; 455: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 456: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2911054922, ; 457: Microsoft.Extensions.FileProviders.Physical.dll => 0xad832c4a => 187
	i32 2916838712, ; 458: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 275
	i32 2919462931, ; 459: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 460: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 214
	i32 2936416060, ; 461: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 462: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 463: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2959614098, ; 464: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 465: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2971004615, ; 466: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0xb115eec7 => 194
	i32 2972252294, ; 467: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 468: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 234
	i32 2987532451, ; 469: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 265
	i32 2996846495, ; 470: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 247
	i32 3016983068, ; 471: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 267
	i32 3020703001, ; 472: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 184
	i32 3023353419, ; 473: WindowsBase.dll => 0xb434b64b => 165
	i32 3024354802, ; 474: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 242
	i32 3038032645, ; 475: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 324
	i32 3056245963, ; 476: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 264
	i32 3057625584, ; 477: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 255
	i32 3059408633, ; 478: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 479: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 480: System.Threading.Tasks => 0xb755818f => 144
	i32 3077302341, ; 481: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 302
	i32 3090735792, ; 482: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 483: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 484: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 485: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3121463068, ; 486: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 487: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 488: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3147165239, ; 489: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 490: GoogleGson.dll => 0xbba64c02 => 175
	i32 3159123045, ; 491: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 492: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3166219683, ; 493: MiviaMaui.dll => 0xbcb8ada3 => 0
	i32 3178803400, ; 494: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 256
	i32 3192346100, ; 495: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 496: System.Web => 0xbe592c0c => 153
	i32 3204380047, ; 497: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 498: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 499: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 233
	i32 3220365878, ; 500: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 501: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 502: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 503: Xamarin.AndroidX.CardView => 0xc235e84d => 221
	i32 3265493905, ; 504: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 505: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 506: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 507: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 508: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3286872994, ; 509: SQLite-net.dll => 0xc3e9b3a2 => 202
	i32 3290767353, ; 510: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 511: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 512: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3305363605, ; 513: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 297
	i32 3316684772, ; 514: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 515: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 231
	i32 3317144872, ; 516: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 517: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 219
	i32 3345895724, ; 518: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 260
	i32 3346324047, ; 519: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 257
	i32 3357674450, ; 520: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 314
	i32 3358260929, ; 521: System.Text.Json => 0xc82afec1 => 137
	i32 3360279109, ; 522: SQLitePCLRaw.core => 0xc849ca45 => 204
	i32 3362336904, ; 523: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 212
	i32 3362522851, ; 524: Xamarin.AndroidX.Core => 0xc86c06e3 => 228
	i32 3366347497, ; 525: Java.Interop => 0xc8a662e9 => 168
	i32 3374999561, ; 526: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 261
	i32 3381016424, ; 527: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 293
	i32 3395150330, ; 528: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 529: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3405233483, ; 530: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 232
	i32 3421170118, ; 531: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 178
	i32 3428513518, ; 532: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 182
	i32 3429136800, ; 533: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 534: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 535: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 235
	i32 3445260447, ; 536: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 537: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 196
	i32 3463511458, ; 538: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 301
	i32 3471940407, ; 539: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 540: Mono.Android => 0xcf3163e6 => 171
	i32 3479583265, ; 541: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 314
	i32 3484440000, ; 542: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 313
	i32 3485117614, ; 543: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 544: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 545: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 224
	i32 3509114376, ; 546: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 547: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 548: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 549: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3560100363, ; 550: System.Threading.Timer => 0xd432d20b => 147
	i32 3570554715, ; 551: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3580758918, ; 552: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 321
	i32 3597029428, ; 553: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 210
	i32 3598340787, ; 554: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 555: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 556: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 557: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 259
	i32 3633644679, ; 558: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 214
	i32 3638274909, ; 559: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 560: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 245
	i32 3643446276, ; 561: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 318
	i32 3643854240, ; 562: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 256
	i32 3645089577, ; 563: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 564: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 177
	i32 3660523487, ; 565: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3672681054, ; 566: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3682565725, ; 567: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 220
	i32 3684561358, ; 568: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 224
	i32 3697841164, ; 569: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 323
	i32 3700866549, ; 570: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 571: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 229
	i32 3716563718, ; 572: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 573: Xamarin.AndroidX.Annotation => 0xdda814c6 => 213
	i32 3722202641, ; 574: Microsoft.Extensions.Configuration.Json.dll => 0xdddc4e11 => 180
	i32 3724971120, ; 575: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 255
	i32 3732100267, ; 576: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 577: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 578: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 579: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3754567612, ; 580: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 206
	i32 3758424670, ; 581: Microsoft.Extensions.Configuration.FileExtensions => 0xe005025e => 179
	i32 3786282454, ; 582: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 222
	i32 3792276235, ; 583: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 584: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 196
	i32 3802395368, ; 585: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3817368567, ; 586: CommunityToolkit.Maui.dll => 0xe3886bf7 => 173
	i32 3819260425, ; 587: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 588: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 589: System.Numerics.dll => 0xe4436460 => 83
	i32 3841636137, ; 590: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 183
	i32 3844307129, ; 591: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 592: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3870376305, ; 593: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 594: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 595: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3876362041, ; 596: SQLite-net => 0xe70c9739 => 202
	i32 3885497537, ; 597: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 598: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 270
	i32 3888767677, ; 599: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 260
	i32 3889960447, ; 600: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 322
	i32 3896106733, ; 601: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 602: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 228
	i32 3901907137, ; 603: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 604: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 605: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 273
	i32 3928044579, ; 606: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 607: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3931092270, ; 608: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 258
	i32 3945713374, ; 609: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 610: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 611: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 216
	i32 3959773229, ; 612: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 247
	i32 3963903781, ; 613: Microsoft.Extensions.Configuration.UserSecrets => 0xec445f25 => 181
	i32 3980434154, ; 614: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 317
	i32 3987592930, ; 615: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 299
	i32 4003436829, ; 616: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 617: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 215
	i32 4025784931, ; 618: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 619: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 198
	i32 4054681211, ; 620: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 621: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 622: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4094352644, ; 623: Microsoft.Maui.Essentials.dll => 0xf40add04 => 200
	i32 4099507663, ; 624: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 625: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 626: Xamarin.AndroidX.Emoji2 => 0xf479582c => 236
	i32 4102112229, ; 627: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 312
	i32 4125707920, ; 628: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 307
	i32 4126470640, ; 629: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 182
	i32 4127667938, ; 630: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 631: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 632: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4150914736, ; 633: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 319
	i32 4151237749, ; 634: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 635: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 636: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 637: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 638: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 639: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 252
	i32 4185676441, ; 640: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 641: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 642: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4256097574, ; 643: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 229
	i32 4258378803, ; 644: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 251
	i32 4260525087, ; 645: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 646: Microsoft.Maui.Controls.dll => 0xfea12dee => 197
	i32 4274976490, ; 647: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4292120959, ; 648: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 252
	i32 4294763496 ; 649: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 238
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [650 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 108, ; 2
	i32 248, ; 3
	i32 282, ; 4
	i32 48, ; 5
	i32 80, ; 6
	i32 145, ; 7
	i32 30, ; 8
	i32 323, ; 9
	i32 124, ; 10
	i32 201, ; 11
	i32 0, ; 12
	i32 102, ; 13
	i32 185, ; 14
	i32 266, ; 15
	i32 107, ; 16
	i32 266, ; 17
	i32 139, ; 18
	i32 286, ; 19
	i32 77, ; 20
	i32 124, ; 21
	i32 13, ; 22
	i32 222, ; 23
	i32 132, ; 24
	i32 268, ; 25
	i32 151, ; 26
	i32 320, ; 27
	i32 321, ; 28
	i32 18, ; 29
	i32 220, ; 30
	i32 26, ; 31
	i32 184, ; 32
	i32 242, ; 33
	i32 1, ; 34
	i32 59, ; 35
	i32 42, ; 36
	i32 91, ; 37
	i32 225, ; 38
	i32 147, ; 39
	i32 244, ; 40
	i32 241, ; 41
	i32 292, ; 42
	i32 54, ; 43
	i32 189, ; 44
	i32 69, ; 45
	i32 320, ; 46
	i32 211, ; 47
	i32 83, ; 48
	i32 305, ; 49
	i32 243, ; 50
	i32 205, ; 51
	i32 304, ; 52
	i32 131, ; 53
	i32 55, ; 54
	i32 149, ; 55
	i32 74, ; 56
	i32 145, ; 57
	i32 62, ; 58
	i32 146, ; 59
	i32 324, ; 60
	i32 165, ; 61
	i32 316, ; 62
	i32 226, ; 63
	i32 12, ; 64
	i32 239, ; 65
	i32 125, ; 66
	i32 152, ; 67
	i32 113, ; 68
	i32 166, ; 69
	i32 164, ; 70
	i32 241, ; 71
	i32 254, ; 72
	i32 84, ; 73
	i32 303, ; 74
	i32 297, ; 75
	i32 195, ; 76
	i32 150, ; 77
	i32 286, ; 78
	i32 60, ; 79
	i32 190, ; 80
	i32 51, ; 81
	i32 103, ; 82
	i32 114, ; 83
	i32 40, ; 84
	i32 279, ; 85
	i32 277, ; 86
	i32 120, ; 87
	i32 311, ; 88
	i32 173, ; 89
	i32 52, ; 90
	i32 44, ; 91
	i32 119, ; 92
	i32 231, ; 93
	i32 309, ; 94
	i32 237, ; 95
	i32 81, ; 96
	i32 136, ; 97
	i32 273, ; 98
	i32 218, ; 99
	i32 8, ; 100
	i32 73, ; 101
	i32 291, ; 102
	i32 155, ; 103
	i32 288, ; 104
	i32 154, ; 105
	i32 92, ; 106
	i32 283, ; 107
	i32 45, ; 108
	i32 306, ; 109
	i32 294, ; 110
	i32 287, ; 111
	i32 109, ; 112
	i32 194, ; 113
	i32 129, ; 114
	i32 203, ; 115
	i32 25, ; 116
	i32 208, ; 117
	i32 72, ; 118
	i32 55, ; 119
	i32 46, ; 120
	i32 315, ; 121
	i32 193, ; 122
	i32 232, ; 123
	i32 22, ; 124
	i32 246, ; 125
	i32 86, ; 126
	i32 43, ; 127
	i32 160, ; 128
	i32 71, ; 129
	i32 259, ; 130
	i32 3, ; 131
	i32 42, ; 132
	i32 63, ; 133
	i32 16, ; 134
	i32 53, ; 135
	i32 318, ; 136
	i32 282, ; 137
	i32 105, ; 138
	i32 287, ; 139
	i32 280, ; 140
	i32 243, ; 141
	i32 34, ; 142
	i32 158, ; 143
	i32 85, ; 144
	i32 32, ; 145
	i32 12, ; 146
	i32 51, ; 147
	i32 188, ; 148
	i32 56, ; 149
	i32 263, ; 150
	i32 36, ; 151
	i32 183, ; 152
	i32 293, ; 153
	i32 281, ; 154
	i32 216, ; 155
	i32 35, ; 156
	i32 58, ; 157
	i32 185, ; 158
	i32 250, ; 159
	i32 175, ; 160
	i32 17, ; 161
	i32 284, ; 162
	i32 164, ; 163
	i32 179, ; 164
	i32 306, ; 165
	i32 249, ; 166
	i32 192, ; 167
	i32 276, ; 168
	i32 312, ; 169
	i32 153, ; 170
	i32 186, ; 171
	i32 272, ; 172
	i32 257, ; 173
	i32 310, ; 174
	i32 218, ; 175
	i32 29, ; 176
	i32 52, ; 177
	i32 308, ; 178
	i32 277, ; 179
	i32 5, ; 180
	i32 292, ; 181
	i32 267, ; 182
	i32 271, ; 183
	i32 223, ; 184
	i32 288, ; 185
	i32 215, ; 186
	i32 204, ; 187
	i32 234, ; 188
	i32 85, ; 189
	i32 276, ; 190
	i32 61, ; 191
	i32 112, ; 192
	i32 57, ; 193
	i32 322, ; 194
	i32 263, ; 195
	i32 99, ; 196
	i32 19, ; 197
	i32 227, ; 198
	i32 111, ; 199
	i32 101, ; 200
	i32 102, ; 201
	i32 290, ; 202
	i32 104, ; 203
	i32 280, ; 204
	i32 71, ; 205
	i32 38, ; 206
	i32 32, ; 207
	i32 103, ; 208
	i32 73, ; 209
	i32 296, ; 210
	i32 9, ; 211
	i32 123, ; 212
	i32 46, ; 213
	i32 217, ; 214
	i32 195, ; 215
	i32 9, ; 216
	i32 43, ; 217
	i32 4, ; 218
	i32 264, ; 219
	i32 300, ; 220
	i32 189, ; 221
	i32 295, ; 222
	i32 188, ; 223
	i32 31, ; 224
	i32 138, ; 225
	i32 92, ; 226
	i32 93, ; 227
	i32 315, ; 228
	i32 49, ; 229
	i32 141, ; 230
	i32 112, ; 231
	i32 140, ; 232
	i32 233, ; 233
	i32 115, ; 234
	i32 281, ; 235
	i32 157, ; 236
	i32 76, ; 237
	i32 79, ; 238
	i32 253, ; 239
	i32 37, ; 240
	i32 275, ; 241
	i32 180, ; 242
	i32 174, ; 243
	i32 237, ; 244
	i32 230, ; 245
	i32 64, ; 246
	i32 138, ; 247
	i32 15, ; 248
	i32 116, ; 249
	i32 269, ; 250
	i32 278, ; 251
	i32 225, ; 252
	i32 48, ; 253
	i32 70, ; 254
	i32 80, ; 255
	i32 126, ; 256
	i32 94, ; 257
	i32 121, ; 258
	i32 285, ; 259
	i32 26, ; 260
	i32 205, ; 261
	i32 246, ; 262
	i32 97, ; 263
	i32 28, ; 264
	i32 221, ; 265
	i32 313, ; 266
	i32 291, ; 267
	i32 149, ; 268
	i32 169, ; 269
	i32 4, ; 270
	i32 98, ; 271
	i32 33, ; 272
	i32 93, ; 273
	i32 268, ; 274
	i32 181, ; 275
	i32 190, ; 276
	i32 21, ; 277
	i32 41, ; 278
	i32 170, ; 279
	i32 307, ; 280
	i32 239, ; 281
	i32 299, ; 282
	i32 253, ; 283
	i32 284, ; 284
	i32 278, ; 285
	i32 258, ; 286
	i32 2, ; 287
	i32 134, ; 288
	i32 111, ; 289
	i32 191, ; 290
	i32 319, ; 291
	i32 208, ; 292
	i32 316, ; 293
	i32 58, ; 294
	i32 95, ; 295
	i32 298, ; 296
	i32 39, ; 297
	i32 219, ; 298
	i32 25, ; 299
	i32 94, ; 300
	i32 89, ; 301
	i32 99, ; 302
	i32 10, ; 303
	i32 87, ; 304
	i32 100, ; 305
	i32 265, ; 306
	i32 176, ; 307
	i32 285, ; 308
	i32 210, ; 309
	i32 295, ; 310
	i32 7, ; 311
	i32 250, ; 312
	i32 290, ; 313
	i32 207, ; 314
	i32 88, ; 315
	i32 178, ; 316
	i32 245, ; 317
	i32 154, ; 318
	i32 294, ; 319
	i32 33, ; 320
	i32 187, ; 321
	i32 116, ; 322
	i32 82, ; 323
	i32 206, ; 324
	i32 20, ; 325
	i32 11, ; 326
	i32 162, ; 327
	i32 3, ; 328
	i32 199, ; 329
	i32 302, ; 330
	i32 193, ; 331
	i32 191, ; 332
	i32 84, ; 333
	i32 289, ; 334
	i32 64, ; 335
	i32 304, ; 336
	i32 272, ; 337
	i32 143, ; 338
	i32 254, ; 339
	i32 157, ; 340
	i32 41, ; 341
	i32 117, ; 342
	i32 177, ; 343
	i32 209, ; 344
	i32 298, ; 345
	i32 261, ; 346
	i32 131, ; 347
	i32 75, ; 348
	i32 66, ; 349
	i32 308, ; 350
	i32 172, ; 351
	i32 213, ; 352
	i32 143, ; 353
	i32 106, ; 354
	i32 151, ; 355
	i32 70, ; 356
	i32 156, ; 357
	i32 176, ; 358
	i32 121, ; 359
	i32 127, ; 360
	i32 303, ; 361
	i32 152, ; 362
	i32 236, ; 363
	i32 141, ; 364
	i32 223, ; 365
	i32 300, ; 366
	i32 20, ; 367
	i32 14, ; 368
	i32 135, ; 369
	i32 75, ; 370
	i32 59, ; 371
	i32 203, ; 372
	i32 226, ; 373
	i32 167, ; 374
	i32 168, ; 375
	i32 197, ; 376
	i32 15, ; 377
	i32 74, ; 378
	i32 6, ; 379
	i32 23, ; 380
	i32 248, ; 381
	i32 207, ; 382
	i32 91, ; 383
	i32 301, ; 384
	i32 1, ; 385
	i32 136, ; 386
	i32 249, ; 387
	i32 271, ; 388
	i32 134, ; 389
	i32 69, ; 390
	i32 146, ; 391
	i32 186, ; 392
	i32 310, ; 393
	i32 289, ; 394
	i32 240, ; 395
	i32 192, ; 396
	i32 88, ; 397
	i32 96, ; 398
	i32 230, ; 399
	i32 235, ; 400
	i32 305, ; 401
	i32 31, ; 402
	i32 45, ; 403
	i32 244, ; 404
	i32 209, ; 405
	i32 109, ; 406
	i32 158, ; 407
	i32 35, ; 408
	i32 22, ; 409
	i32 114, ; 410
	i32 57, ; 411
	i32 269, ; 412
	i32 144, ; 413
	i32 118, ; 414
	i32 120, ; 415
	i32 110, ; 416
	i32 211, ; 417
	i32 139, ; 418
	i32 217, ; 419
	i32 54, ; 420
	i32 105, ; 421
	i32 311, ; 422
	i32 198, ; 423
	i32 199, ; 424
	i32 133, ; 425
	i32 283, ; 426
	i32 274, ; 427
	i32 262, ; 428
	i32 317, ; 429
	i32 240, ; 430
	i32 201, ; 431
	i32 159, ; 432
	i32 296, ; 433
	i32 227, ; 434
	i32 163, ; 435
	i32 132, ; 436
	i32 262, ; 437
	i32 161, ; 438
	i32 309, ; 439
	i32 251, ; 440
	i32 140, ; 441
	i32 274, ; 442
	i32 270, ; 443
	i32 169, ; 444
	i32 200, ; 445
	i32 174, ; 446
	i32 212, ; 447
	i32 279, ; 448
	i32 40, ; 449
	i32 238, ; 450
	i32 81, ; 451
	i32 56, ; 452
	i32 37, ; 453
	i32 97, ; 454
	i32 166, ; 455
	i32 172, ; 456
	i32 187, ; 457
	i32 275, ; 458
	i32 82, ; 459
	i32 214, ; 460
	i32 98, ; 461
	i32 30, ; 462
	i32 159, ; 463
	i32 18, ; 464
	i32 127, ; 465
	i32 194, ; 466
	i32 119, ; 467
	i32 234, ; 468
	i32 265, ; 469
	i32 247, ; 470
	i32 267, ; 471
	i32 184, ; 472
	i32 165, ; 473
	i32 242, ; 474
	i32 324, ; 475
	i32 264, ; 476
	i32 255, ; 477
	i32 170, ; 478
	i32 16, ; 479
	i32 144, ; 480
	i32 302, ; 481
	i32 125, ; 482
	i32 118, ; 483
	i32 38, ; 484
	i32 115, ; 485
	i32 47, ; 486
	i32 142, ; 487
	i32 117, ; 488
	i32 34, ; 489
	i32 175, ; 490
	i32 95, ; 491
	i32 53, ; 492
	i32 0, ; 493
	i32 256, ; 494
	i32 129, ; 495
	i32 153, ; 496
	i32 24, ; 497
	i32 161, ; 498
	i32 233, ; 499
	i32 148, ; 500
	i32 104, ; 501
	i32 89, ; 502
	i32 221, ; 503
	i32 60, ; 504
	i32 142, ; 505
	i32 100, ; 506
	i32 5, ; 507
	i32 13, ; 508
	i32 202, ; 509
	i32 122, ; 510
	i32 135, ; 511
	i32 28, ; 512
	i32 297, ; 513
	i32 72, ; 514
	i32 231, ; 515
	i32 24, ; 516
	i32 219, ; 517
	i32 260, ; 518
	i32 257, ; 519
	i32 314, ; 520
	i32 137, ; 521
	i32 204, ; 522
	i32 212, ; 523
	i32 228, ; 524
	i32 168, ; 525
	i32 261, ; 526
	i32 293, ; 527
	i32 101, ; 528
	i32 123, ; 529
	i32 232, ; 530
	i32 178, ; 531
	i32 182, ; 532
	i32 163, ; 533
	i32 167, ; 534
	i32 235, ; 535
	i32 39, ; 536
	i32 196, ; 537
	i32 301, ; 538
	i32 17, ; 539
	i32 171, ; 540
	i32 314, ; 541
	i32 313, ; 542
	i32 137, ; 543
	i32 150, ; 544
	i32 224, ; 545
	i32 155, ; 546
	i32 130, ; 547
	i32 19, ; 548
	i32 65, ; 549
	i32 147, ; 550
	i32 47, ; 551
	i32 321, ; 552
	i32 210, ; 553
	i32 79, ; 554
	i32 61, ; 555
	i32 106, ; 556
	i32 259, ; 557
	i32 214, ; 558
	i32 49, ; 559
	i32 245, ; 560
	i32 318, ; 561
	i32 256, ; 562
	i32 14, ; 563
	i32 177, ; 564
	i32 68, ; 565
	i32 171, ; 566
	i32 220, ; 567
	i32 224, ; 568
	i32 323, ; 569
	i32 78, ; 570
	i32 229, ; 571
	i32 108, ; 572
	i32 213, ; 573
	i32 180, ; 574
	i32 255, ; 575
	i32 67, ; 576
	i32 63, ; 577
	i32 27, ; 578
	i32 160, ; 579
	i32 206, ; 580
	i32 179, ; 581
	i32 222, ; 582
	i32 10, ; 583
	i32 196, ; 584
	i32 11, ; 585
	i32 173, ; 586
	i32 78, ; 587
	i32 126, ; 588
	i32 83, ; 589
	i32 183, ; 590
	i32 66, ; 591
	i32 107, ; 592
	i32 65, ; 593
	i32 128, ; 594
	i32 122, ; 595
	i32 202, ; 596
	i32 77, ; 597
	i32 270, ; 598
	i32 260, ; 599
	i32 322, ; 600
	i32 8, ; 601
	i32 228, ; 602
	i32 2, ; 603
	i32 44, ; 604
	i32 273, ; 605
	i32 156, ; 606
	i32 128, ; 607
	i32 258, ; 608
	i32 23, ; 609
	i32 133, ; 610
	i32 216, ; 611
	i32 247, ; 612
	i32 181, ; 613
	i32 317, ; 614
	i32 299, ; 615
	i32 29, ; 616
	i32 215, ; 617
	i32 62, ; 618
	i32 198, ; 619
	i32 90, ; 620
	i32 87, ; 621
	i32 148, ; 622
	i32 200, ; 623
	i32 36, ; 624
	i32 86, ; 625
	i32 236, ; 626
	i32 312, ; 627
	i32 307, ; 628
	i32 182, ; 629
	i32 50, ; 630
	i32 6, ; 631
	i32 90, ; 632
	i32 319, ; 633
	i32 21, ; 634
	i32 162, ; 635
	i32 96, ; 636
	i32 50, ; 637
	i32 113, ; 638
	i32 252, ; 639
	i32 130, ; 640
	i32 76, ; 641
	i32 27, ; 642
	i32 229, ; 643
	i32 251, ; 644
	i32 7, ; 645
	i32 197, ; 646
	i32 110, ; 647
	i32 252, ; 648
	i32 238 ; 649
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 0d97e20b84d8e87c3502469ee395805907905fe3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
