; ModuleID = 'marshal_methods.arm64-v8a.ll'
source_filename = "marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [336 x ptr] zeroinitializer, align 8

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [672 x i64] [
	i64 24362543149721218, ; 0: Xamarin.AndroidX.DynamicAnimation => 0x568d9a9a43a682 => 244
	i64 36418902923615093, ; 1: Plugin.LocalNotification => 0x8162cc9bdf1b75 => 207
	i64 98382396393917666, ; 2: Microsoft.Extensions.Primitives.dll => 0x15d8644ad360ce2 => 200
	i64 120698629574877762, ; 3: Mono.Android => 0x1accec39cafe242 => 173
	i64 131669012237370309, ; 4: Microsoft.Maui.Essentials.dll => 0x1d3c844de55c3c5 => 205
	i64 196720943101637631, ; 5: System.Linq.Expressions.dll => 0x2bae4a7cd73f3ff => 60
	i64 210515253464952879, ; 6: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 230
	i64 229794953483747371, ; 7: System.ValueTuple.dll => 0x330654aed93802b => 153
	i64 232391251801502327, ; 8: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 272
	i64 233177144301842968, ; 9: Xamarin.AndroidX.Collection.Jvm.dll => 0x33c696097d9f218 => 231
	i64 295915112840604065, ; 10: Xamarin.AndroidX.SlidingPaneLayout => 0x41b4d3a3088a9a1 => 275
	i64 316157742385208084, ; 11: Xamarin.AndroidX.Core.Core.Ktx.dll => 0x46337caa7dc1b14 => 238
	i64 350667413455104241, ; 12: System.ServiceProcess.dll => 0x4ddd227954be8f1 => 134
	i64 354178770117062970, ; 13: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0x4ea4bb703cff13a => 199
	i64 422779754995088667, ; 14: System.IO.UnmanagedMemoryStream => 0x5de03f27ab57d1b => 58
	i64 435118502366263740, ; 15: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x609d9f8f8bdb9bc => 274
	i64 435170709725415398, ; 16: Xamarin.GooglePlayServices.Location => 0x60a097471d687e6 => 294
	i64 545109961164950392, ; 17: fi/Microsoft.Maui.Controls.resources.dll => 0x7909e9f1ec38b78 => 308
	i64 560278790331054453, ; 18: System.Reflection.Primitives => 0x7c6829760de3975 => 97
	i64 634308326490598313, ; 19: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 257
	i64 648449422406355874, ; 20: Microsoft.Extensions.Configuration.FileExtensions.dll => 0x8ffc15065568ba2 => 184
	i64 649145001856603771, ; 21: System.Security.SecureString => 0x90239f09b62167b => 131
	i64 668723562677762733, ; 22: Microsoft.Extensions.Configuration.Binder.dll => 0x947c88986577aad => 183
	i64 687654259221141486, ; 23: Xamarin.GooglePlayServices.Base => 0x98b09e7c92917ee => 292
	i64 750875890346172408, ; 24: System.Threading.Thread => 0xa6ba5a4da7d1ff8 => 147
	i64 798450721097591769, ; 25: Xamarin.AndroidX.Collection.Ktx.dll => 0xb14aab351ad2bd9 => 232
	i64 799765834175365804, ; 26: System.ComponentModel.dll => 0xb1956c9f18442ac => 20
	i64 849051935479314978, ; 27: hi/Microsoft.Maui.Controls.resources.dll => 0xbc8703ca21a3a22 => 311
	i64 870603111519317375, ; 28: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 212
	i64 872800313462103108, ; 29: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 243
	i64 895210737996778430, ; 30: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0xc6c6d6c5569cbbe => 258
	i64 940822596282819491, ; 31: System.Transactions => 0xd0e792aa81923a3 => 152
	i64 960778385402502048, ; 32: System.Runtime.Handles.dll => 0xd555ed9e1ca1ba0 => 106
	i64 1010599046655515943, ; 33: System.Reflection.Primitives.dll => 0xe065e7a82401d27 => 97
	i64 1120440138749646132, ; 34: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 287
	i64 1121665720830085036, ; 35: nb/Microsoft.Maui.Controls.resources.dll => 0xf90f507becf47ac => 319
	i64 1268860745194512059, ; 36: System.Drawing.dll => 0x119be62002c19ebb => 38
	i64 1301485588176585670, ; 37: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 211
	i64 1301626418029409250, ; 38: System.Diagnostics.FileVersionInfo => 0x12104e54b4e833e2 => 30
	i64 1315114680217950157, ; 39: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 226
	i64 1369545283391376210, ; 40: Xamarin.AndroidX.Navigation.Fragment.dll => 0x13019a2dd85acb52 => 265
	i64 1404195534211153682, ; 41: System.IO.FileSystem.Watcher.dll => 0x137cb4660bd87f12 => 52
	i64 1425944114962822056, ; 42: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 117
	i64 1476839205573959279, ; 43: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 72
	i64 1486715745332614827, ; 44: Microsoft.Maui.Controls.dll => 0x14a1e017ea87d6ab => 202
	i64 1492954217099365037, ; 45: System.Net.HttpListener => 0x14b809f350210aad => 67
	i64 1513467482682125403, ; 46: Mono.Android.Runtime => 0x1500eaa8245f6c5b => 172
	i64 1518315023656898250, ; 47: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 213
	i64 1537168428375924959, ; 48: System.Threading.Thread.dll => 0x15551e8a954ae0df => 147
	i64 1556147632182429976, ; 49: ko/Microsoft.Maui.Controls.resources.dll => 0x15988c06d24c8918 => 317
	i64 1576750169145655260, ; 50: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x15e1bdecc376bfdc => 286
	i64 1624659445732251991, ; 51: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 225
	i64 1628611045998245443, ; 52: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0x1699fd1e1a00b643 => 261
	i64 1636321030536304333, ; 53: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0x16b5614ec39e16cd => 251
	i64 1651782184287836205, ; 54: System.Globalization.Calendars => 0x16ec4f2524cb982d => 42
	i64 1659332977923810219, ; 55: System.Reflection.DispatchProxy => 0x1707228d493d63ab => 91
	i64 1682513316613008342, ; 56: System.Net.dll => 0x17597cf276952bd6 => 83
	i64 1735388228521408345, ; 57: System.Net.Mail.dll => 0x181556663c69b759 => 68
	i64 1743969030606105336, ; 58: System.Memory.dll => 0x1833d297e88f2af8 => 64
	i64 1767386781656293639, ; 59: System.Private.Uri.dll => 0x188704e9f5582107 => 88
	i64 1795316252682057001, ; 60: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 224
	i64 1825687700144851180, ; 61: System.Runtime.InteropServices.RuntimeInformation.dll => 0x1956254a55ef08ec => 108
	i64 1835311033149317475, ; 62: es\Microsoft.Maui.Controls.resources => 0x197855a927386163 => 307
	i64 1836611346387731153, ; 63: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 272
	i64 1854145951182283680, ; 64: System.Runtime.CompilerServices.VisualC => 0x19bb3feb3df2e3a0 => 104
	i64 1875417405349196092, ; 65: System.Drawing.Primitives => 0x1a06d2319b6c713c => 37
	i64 1875917498431009007, ; 66: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 221
	i64 1881198190668717030, ; 67: tr\Microsoft.Maui.Controls.resources => 0x1a1b5bc992ea9be6 => 329
	i64 1897575647115118287, ; 68: Xamarin.AndroidX.Security.SecurityCrypto => 0x1a558aff4cba86cf => 274
	i64 1920760634179481754, ; 69: Microsoft.Maui.Controls.Xaml => 0x1aa7e99ec2d2709a => 203
	i64 1959996714666907089, ; 70: tr/Microsoft.Maui.Controls.resources.dll => 0x1b334ea0a2a755d1 => 329
	i64 1972385128188460614, ; 71: System.Security.Cryptography.Algorithms => 0x1b5f51d2edefbe46 => 121
	i64 1981742497975770890, ; 72: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 259
	i64 1983698669889758782, ; 73: cs/Microsoft.Maui.Controls.resources.dll => 0x1b87836e2031a63e => 303
	i64 2019660174692588140, ; 74: pl/Microsoft.Maui.Controls.resources.dll => 0x1c07463a6f8e1a6c => 321
	i64 2040001226662520565, ; 75: System.Threading.Tasks.Extensions.dll => 0x1c4f8a4ea894a6f5 => 144
	i64 2062890601515140263, ; 76: System.Threading.Tasks.Dataflow => 0x1ca0dc1289cd44a7 => 143
	i64 2080945842184875448, ; 77: System.IO.MemoryMappedFiles => 0x1ce10137d8416db8 => 55
	i64 2102659300918482391, ; 78: System.Drawing.Primitives.dll => 0x1d2e257e6aead5d7 => 37
	i64 2106033277907880740, ; 79: System.Threading.Tasks.Dataflow.dll => 0x1d3a221ba6d9cb24 => 143
	i64 2165310824878145998, ; 80: Xamarin.Android.Glide.GifDecoder => 0x1e0cbab9112b81ce => 218
	i64 2165725771938924357, ; 81: Xamarin.AndroidX.Browser => 0x1e0e341d75540745 => 228
	i64 2200176636225660136, ; 82: Microsoft.Extensions.Logging.Debug.dll => 0x1e8898fe5d5824e8 => 197
	i64 2203565783020068373, ; 83: Xamarin.KotlinX.Coroutines.Core => 0x1e94a367981dde15 => 299
	i64 2262844636196693701, ; 84: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 243
	i64 2287834202362508563, ; 85: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 10
	i64 2287887973817120656, ; 86: System.ComponentModel.DataAnnotations.dll => 0x1fc035fd8d41f790 => 16
	i64 2302323944321350744, ; 87: ru/Microsoft.Maui.Controls.resources.dll => 0x1ff37f6ddb267c58 => 325
	i64 2304837677853103545, ; 88: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0x1ffc6da80d5ed5b9 => 271
	i64 2315304989185124968, ; 89: System.IO.FileSystem.dll => 0x20219d9ee311aa68 => 53
	i64 2329709569556905518, ; 90: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 254
	i64 2335503487726329082, ; 91: System.Text.Encodings.Web => 0x2069600c4d9d1cfa => 138
	i64 2337758774805907496, ; 92: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 103
	i64 2470498323731680442, ; 93: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 236
	i64 2479423007379663237, ; 94: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 281
	i64 2497223385847772520, ; 95: System.Runtime => 0x22a7eb7046413568 => 118
	i64 2547086958574651984, ; 96: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 219
	i64 2592350477072141967, ; 97: System.Xml.dll => 0x23f9e10627330e8f => 165
	i64 2602673633151553063, ; 98: th\Microsoft.Maui.Controls.resources => 0x241e8de13a460e27 => 328
	i64 2624866290265602282, ; 99: mscorlib.dll => 0x246d65fbde2db8ea => 168
	i64 2632269733008246987, ; 100: System.Net.NameResolution => 0x2487b36034f808cb => 69
	i64 2656907746661064104, ; 101: Microsoft.Extensions.DependencyInjection => 0x24df3b84c8b75da8 => 187
	i64 2662981627730767622, ; 102: cs\Microsoft.Maui.Controls.resources => 0x24f4cfae6c48af06 => 303
	i64 2706075432581334785, ; 103: System.Net.WebSockets => 0x258de944be6c0701 => 82
	i64 2757881497033080458, ; 104: de\MiviaMaui.resources => 0x2645f69c13477e8a => 0
	i64 2783046991838674048, ; 105: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 103
	i64 2787234703088983483, ; 106: Xamarin.AndroidX.Startup.StartupRuntime => 0x26ae3f31ef429dbb => 276
	i64 2815524396660695947, ; 107: System.Security.AccessControl => 0x2712c0857f68238b => 119
	i64 2895129759130297543, ; 108: fi\Microsoft.Maui.Controls.resources => 0x282d912d479fa4c7 => 308
	i64 2923871038697555247, ; 109: Jsr305Binding => 0x2893ad37e69ec52f => 288
	i64 3017136373564924869, ; 110: System.Net.WebProxy => 0x29df058bd93f63c5 => 80
	i64 3017704767998173186, ; 111: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 287
	i64 3106456277381543405, ; 112: MiviaMaui.dll => 0x2b1c59868b9bf5ed => 2
	i64 3106852385031680087, ; 113: System.Runtime.Serialization.Xml => 0x2b1dc1c88b637057 => 116
	i64 3110390492489056344, ; 114: System.Security.Cryptography.Csp.dll => 0x2b2a53ac61900058 => 123
	i64 3135773902340015556, ; 115: System.IO.FileSystem.DriveInfo.dll => 0x2b8481c008eac5c4 => 50
	i64 3281594302220646930, ; 116: System.Security.Principal => 0x2d8a90a198ceba12 => 130
	i64 3289520064315143713, ; 117: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 252
	i64 3303437397778967116, ; 118: Xamarin.AndroidX.Annotation.Experimental => 0x2dd82acf985b2a4c => 222
	i64 3311221304742556517, ; 119: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 84
	i64 3325875462027654285, ; 120: System.Runtime.Numerics => 0x2e27e21c8958b48d => 112
	i64 3328853167529574890, ; 121: System.Net.Sockets.dll => 0x2e327651a008c1ea => 77
	i64 3344514922410554693, ; 122: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 300
	i64 3396143930648122816, ; 123: Microsoft.Extensions.FileProviders.Abstractions => 0x2f2186e9506155c0 => 191
	i64 3411255996856937470, ; 124: Xamarin.GooglePlayServices.Basement => 0x2f5737416a942bfe => 293
	i64 3429672777697402584, ; 125: Microsoft.Maui.Essentials => 0x2f98a5385a7b1ed8 => 205
	i64 3437845325506641314, ; 126: System.IO.MemoryMappedFiles.dll => 0x2fb5ae1beb8f7da2 => 55
	i64 3493805808809882663, ; 127: Xamarin.AndroidX.Tracing.Tracing.dll => 0x307c7ddf444f3427 => 278
	i64 3494946837667399002, ; 128: Microsoft.Extensions.Configuration => 0x30808ba1c00a455a => 181
	i64 3508450208084372758, ; 129: System.Net.Ping => 0x30b084e02d03ad16 => 71
	i64 3518278411901492328, ; 130: pl\MiviaMaui.resources => 0x30d36f9332505c68 => 1
	i64 3522470458906976663, ; 131: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 277
	i64 3523004241079211829, ; 132: Microsoft.Extensions.Caching.Memory.dll => 0x30e439b10bb89735 => 180
	i64 3531994851595924923, ; 133: System.Numerics => 0x31042a9aade235bb => 85
	i64 3551103847008531295, ; 134: System.Private.CoreLib.dll => 0x31480e226177735f => 174
	i64 3567343442040498961, ; 135: pt\Microsoft.Maui.Controls.resources => 0x3181bff5bea4ab11 => 323
	i64 3571415421602489686, ; 136: System.Runtime.dll => 0x319037675df7e556 => 118
	i64 3638003163729360188, ; 137: Microsoft.Extensions.Configuration.Abstractions => 0x327cc89a39d5f53c => 182
	i64 3647754201059316852, ; 138: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 158
	i64 3655542548057982301, ; 139: Microsoft.Extensions.Configuration.dll => 0x32bb18945e52855d => 181
	i64 3659371656528649588, ; 140: Xamarin.Android.Glide.Annotations => 0x32c8b3222885dd74 => 216
	i64 3716579019761409177, ; 141: netstandard.dll => 0x3393f0ed5c8c5c99 => 169
	i64 3727469159507183293, ; 142: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 270
	i64 3772598417116884899, ; 143: Xamarin.AndroidX.DynamicAnimation.dll => 0x345af645b473efa3 => 244
	i64 3853740304185200951, ; 144: MiviaMaui => 0x357b3c637f909137 => 2
	i64 3869221888984012293, ; 145: Microsoft.Extensions.Logging.dll => 0x35b23cceda0ed605 => 195
	i64 3869649043256705283, ; 146: System.Diagnostics.Tools => 0x35b3c14d74bf0103 => 34
	i64 3889433610606858880, ; 147: Microsoft.Extensions.FileProviders.Physical.dll => 0x35fa0b4301afd280 => 192
	i64 3890352374528606784, ; 148: Microsoft.Maui.Controls.Xaml.dll => 0x35fd4edf66e00240 => 203
	i64 3919223565570527920, ; 149: System.Security.Cryptography.Encoding => 0x3663e111652bd2b0 => 124
	i64 3933965368022646939, ; 150: System.Net.Requests => 0x369840a8bfadc09b => 74
	i64 3966267475168208030, ; 151: System.Memory => 0x370b03412596249e => 64
	i64 4006972109285359177, ; 152: System.Xml.XmlDocument => 0x379b9fe74ed9fe49 => 163
	i64 4009997192427317104, ; 153: System.Runtime.Serialization.Primitives => 0x37a65f335cf1a770 => 115
	i64 4073500526318903918, ; 154: System.Private.Xml.dll => 0x3887fb25779ae26e => 90
	i64 4073631083018132676, ; 155: Microsoft.Maui.Controls.Compatibility.dll => 0x388871e311491cc4 => 201
	i64 4120493066591692148, ; 156: zh-Hant\Microsoft.Maui.Controls.resources => 0x392eee9cdda86574 => 334
	i64 4148881117810174540, ; 157: System.Runtime.InteropServices.JavaScript.dll => 0x3993c9651a66aa4c => 107
	i64 4154383907710350974, ; 158: System.ComponentModel => 0x39a7562737acb67e => 20
	i64 4167269041631776580, ; 159: System.Threading.ThreadPool => 0x39d51d1d3df1cf44 => 148
	i64 4168469861834746866, ; 160: System.Security.Claims.dll => 0x39d96140fb94ebf2 => 120
	i64 4187479170553454871, ; 161: System.Linq.Expressions => 0x3a1cea1e912fa117 => 60
	i64 4201423742386704971, ; 162: Xamarin.AndroidX.Core.Core.Ktx => 0x3a4e74a233da124b => 238
	i64 4205801962323029395, ; 163: System.ComponentModel.TypeConverter => 0x3a5e0299f7e7ad93 => 19
	i64 4232544790861428616, ; 164: TiffLibrary.dll => 0x3abd050ff339b788 => 214
	i64 4235503420553921860, ; 165: System.IO.IsolatedStorage.dll => 0x3ac787eb9b118544 => 54
	i64 4247996603072512073, ; 166: Xamarin.GooglePlayServices.Tasks => 0x3af3ea6755340049 => 295
	i64 4282138915307457788, ; 167: System.Reflection.Emit => 0x3b6d36a7ddc70cfc => 94
	i64 4337444564132831293, ; 168: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 210
	i64 4356591372459378815, ; 169: vi/Microsoft.Maui.Controls.resources.dll => 0x3c75b8c562f9087f => 331
	i64 4373617458794931033, ; 170: System.IO.Pipes.dll => 0x3cb235e806eb2359 => 57
	i64 4397634830160618470, ; 171: System.Security.SecureString.dll => 0x3d0789940f9be3e6 => 131
	i64 4477672992252076438, ; 172: System.Web.HttpUtility.dll => 0x3e23e3dcdb8ba196 => 154
	i64 4484706122338676047, ; 173: System.Globalization.Extensions.dll => 0x3e3ce07510042d4f => 43
	i64 4533124835995628778, ; 174: System.Reflection.Emit.dll => 0x3ee8e505540534ea => 94
	i64 4636684751163556186, ; 175: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 282
	i64 4657212095206026001, ; 176: Microsoft.Extensions.Http.dll => 0x40a1bdb9c2686b11 => 194
	i64 4672453897036726049, ; 177: System.IO.FileSystem.Watcher => 0x40d7e4104a437f21 => 52
	i64 4679594760078841447, ; 178: ar/Microsoft.Maui.Controls.resources.dll => 0x40f142a407475667 => 301
	i64 4716677666592453464, ; 179: System.Xml.XmlSerializer => 0x417501590542f358 => 164
	i64 4743821336939966868, ; 180: System.ComponentModel.Annotations => 0x41d5705f4239b194 => 15
	i64 4759461199762736555, ; 181: Xamarin.AndroidX.Lifecycle.Process.dll => 0x420d00be961cc5ab => 256
	i64 4794310189461587505, ; 182: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 219
	i64 4795410492532947900, ; 183: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 277
	i64 4809057822547766521, ; 184: System.Drawing => 0x42bd349c3145ecf9 => 38
	i64 4814660307502931973, ; 185: System.Net.NameResolution.dll => 0x42d11c0a5ee2a005 => 69
	i64 4853321196694829351, ; 186: System.Runtime.Loader.dll => 0x435a75ea15de7927 => 111
	i64 5055365687667823624, ; 187: Xamarin.AndroidX.Activity.Ktx.dll => 0x4628444ef7239408 => 220
	i64 5081566143765835342, ; 188: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 101
	i64 5099468265966638712, ; 189: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 101
	i64 5103417709280584325, ; 190: System.Collections.Specialized => 0x46d2fb5e161b6285 => 13
	i64 5182934613077526976, ; 191: System.Collections.Specialized.dll => 0x47ed7b91fa9009c0 => 13
	i64 5205316157927637098, ; 192: Xamarin.AndroidX.LocalBroadcastManager => 0x483cff7778e0c06a => 263
	i64 5244375036463807528, ; 193: System.Diagnostics.Contracts.dll => 0x48c7c34f4d59fc28 => 27
	i64 5262971552273843408, ; 194: System.Security.Principal.dll => 0x4909d4be0c44c4d0 => 130
	i64 5278787618751394462, ; 195: System.Net.WebClient.dll => 0x4942055efc68329e => 78
	i64 5280980186044710147, ; 196: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x4949cf7fd7123d03 => 255
	i64 5290786973231294105, ; 197: System.Runtime.Loader => 0x496ca6b869b72699 => 111
	i64 5376510917114486089, ; 198: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 281
	i64 5408338804355907810, ; 199: Xamarin.AndroidX.Transition => 0x4b0e477cea9840e2 => 279
	i64 5421129952927902830, ; 200: TiffLibrary => 0x4b3bb8f8457f6c6e => 214
	i64 5423376490970181369, ; 201: System.Runtime.InteropServices.RuntimeInformation => 0x4b43b42f2b7b6ef9 => 108
	i64 5440320908473006344, ; 202: Microsoft.VisualBasic.Core => 0x4b7fe70acda9f908 => 4
	i64 5446034149219586269, ; 203: System.Diagnostics.Debug => 0x4b94333452e150dd => 28
	i64 5451019430259338467, ; 204: Xamarin.AndroidX.ConstraintLayout.dll => 0x4ba5e94a845c2ce3 => 234
	i64 5457765010617926378, ; 205: System.Xml.Serialization => 0x4bbde05c557002ea => 159
	i64 5471532531798518949, ; 206: sv\Microsoft.Maui.Controls.resources => 0x4beec9d926d82ca5 => 327
	i64 5507995362134886206, ; 207: System.Core.dll => 0x4c705499688c873e => 23
	i64 5522859530602327440, ; 208: uk\Microsoft.Maui.Controls.resources => 0x4ca5237b51eead90 => 330
	i64 5527431512186326818, ; 209: System.IO.FileSystem.Primitives.dll => 0x4cb561acbc2a8f22 => 51
	i64 5528247634813456972, ; 210: Plugin.LocalNotification.dll => 0x4cb847ef1773124c => 207
	i64 5570799893513421663, ; 211: System.IO.Compression.Brotli => 0x4d4f74fcdfa6c35f => 45
	i64 5573260873512690141, ; 212: System.Security.Cryptography.dll => 0x4d58333c6e4ea1dd => 128
	i64 5574231584441077149, ; 213: Xamarin.AndroidX.Annotation.Jvm => 0x4d5ba617ae5f8d9d => 223
	i64 5591791169662171124, ; 214: System.Linq.Parallel => 0x4d9a087135e137f4 => 61
	i64 5624375662354994915, ; 215: SixLabors.ImageSharp.dll => 0x4e0dcbdd9e0596e3 => 208
	i64 5650097808083101034, ; 216: System.Security.Cryptography.Algorithms.dll => 0x4e692e055d01a56a => 121
	i64 5692067934154308417, ; 217: Xamarin.AndroidX.ViewPager2.dll => 0x4efe49a0d4a8bb41 => 284
	i64 5724799082821825042, ; 218: Xamarin.AndroidX.ExifInterface => 0x4f72926f3e13b212 => 247
	i64 5757522595884336624, ; 219: Xamarin.AndroidX.Concurrent.Futures.dll => 0x4fe6d44bd9f885f0 => 233
	i64 5783556987928984683, ; 220: Microsoft.VisualBasic => 0x504352701bbc3c6b => 5
	i64 5896680224035167651, ; 221: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x51d5376bfbafdda3 => 253
	i64 5959344983920014087, ; 222: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x52b3d8b05c8ef307 => 273
	i64 5979151488806146654, ; 223: System.Formats.Asn1 => 0x52fa3699a489d25e => 40
	i64 5984759512290286505, ; 224: System.Security.Cryptography.Primitives => 0x530e23115c33dba9 => 126
	i64 6010974535988770325, ; 225: Microsoft.Extensions.Diagnostics.dll => 0x536b457e33877615 => 189
	i64 6068057819846744445, ; 226: ro/Microsoft.Maui.Controls.resources.dll => 0x5436126fec7f197d => 324
	i64 6102788177522843259, ; 227: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0x54b1758374b3de7b => 273
	i64 6183170893902868313, ; 228: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 210
	i64 6200764641006662125, ; 229: ro\Microsoft.Maui.Controls.resources => 0x560d8a96830131ed => 324
	i64 6222399776351216807, ; 230: System.Text.Json.dll => 0x565a67a0ffe264a7 => 139
	i64 6251069312384999852, ; 231: System.Transactions.Local => 0x56c0426b870da1ac => 151
	i64 6278736998281604212, ; 232: System.Private.DataContractSerialization => 0x57228e08a4ad6c74 => 87
	i64 6284145129771520194, ; 233: System.Reflection.Emit.ILGeneration => 0x5735c4b3610850c2 => 92
	i64 6319713645133255417, ; 234: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 257
	i64 6357457916754632952, ; 235: _Microsoft.Android.Resource.Designer => 0x583a3a4ac2a7a0f8 => 335
	i64 6401687960814735282, ; 236: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 254
	i64 6478287442656530074, ; 237: hr\Microsoft.Maui.Controls.resources => 0x59e7801b0c6a8e9a => 312
	i64 6504860066809920875, ; 238: Xamarin.AndroidX.Browser.dll => 0x5a45e7c43bd43d6b => 228
	i64 6548213210057960872, ; 239: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 240
	i64 6557084851308642443, ; 240: Xamarin.AndroidX.Window.dll => 0x5aff71ee6c58c08b => 285
	i64 6560151584539558821, ; 241: Microsoft.Extensions.Options => 0x5b0a571be53243a5 => 198
	i64 6589202984700901502, ; 242: Xamarin.Google.ErrorProne.Annotations.dll => 0x5b718d34180a787e => 290
	i64 6591971792923354531, ; 243: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x5b7b636b7e9765a3 => 255
	i64 6617685658146568858, ; 244: System.Text.Encoding.CodePages => 0x5bd6be0b4905fa9a => 135
	i64 6713440830605852118, ; 245: System.Reflection.TypeExtensions.dll => 0x5d2aeeddb8dd7dd6 => 98
	i64 6739853162153639747, ; 246: Microsoft.VisualBasic.dll => 0x5d88c4bde075ff43 => 5
	i64 6743165466166707109, ; 247: nl\Microsoft.Maui.Controls.resources => 0x5d948943c08c43a5 => 320
	i64 6772837112740759457, ; 248: System.Runtime.InteropServices.JavaScript => 0x5dfdf378527ec7a1 => 107
	i64 6777482997383978746, ; 249: pt/Microsoft.Maui.Controls.resources.dll => 0x5e0e74e0a2525efa => 323
	i64 6786606130239981554, ; 250: System.Diagnostics.TraceSource => 0x5e2ede51877147f2 => 35
	i64 6798329586179154312, ; 251: System.Windows => 0x5e5884bd523ca188 => 156
	i64 6814185388980153342, ; 252: System.Xml.XDocument.dll => 0x5e90d98217d1abfe => 160
	i64 6876862101832370452, ; 253: System.Xml.Linq => 0x5f6f85a57d108914 => 157
	i64 6894844156784520562, ; 254: System.Numerics.Vectors => 0x5faf683aead1ad72 => 84
	i64 7011053663211085209, ; 255: Xamarin.AndroidX.Fragment.Ktx => 0x614c442918e5dd99 => 249
	i64 7060896174307865760, ; 256: System.Threading.Tasks.Parallel.dll => 0x61fd57a90988f4a0 => 145
	i64 7083547580668757502, ; 257: System.Private.Xml.Linq.dll => 0x624dd0fe8f56c5fe => 89
	i64 7101497697220435230, ; 258: System.Configuration => 0x628d9687c0141d1e => 21
	i64 7103753931438454322, ; 259: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 250
	i64 7112547816752919026, ; 260: System.IO.FileSystem => 0x62b4d88e3189b1f2 => 53
	i64 7192745174564810625, ; 261: Xamarin.Android.Glide.GifDecoder.dll => 0x63d1c3a0a1d72f81 => 218
	i64 7220009545223068405, ; 262: sv/Microsoft.Maui.Controls.resources.dll => 0x6432a06d99f35af5 => 327
	i64 7270811800166795866, ; 263: System.Linq => 0x64e71ccf51a90a5a => 63
	i64 7299370801165188114, ; 264: System.IO.Pipes.AccessControl.dll => 0x654c9311e74f3c12 => 56
	i64 7316205155833392065, ; 265: Microsoft.Win32.Primitives => 0x658861d38954abc1 => 6
	i64 7338192458477945005, ; 266: System.Reflection => 0x65d67f295d0740ad => 99
	i64 7349431895026339542, ; 267: Xamarin.Android.Glide.DiskLruCache => 0x65fe6d5e9bf88ed6 => 217
	i64 7377312882064240630, ; 268: System.ComponentModel.TypeConverter.dll => 0x66617afac45a2ff6 => 19
	i64 7488575175965059935, ; 269: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 157
	i64 7489048572193775167, ; 270: System.ObjectModel => 0x67ee71ff6b419e3f => 86
	i64 7592577537120840276, ; 271: System.Diagnostics.Process => 0x695e410af5b2aa54 => 31
	i64 7637303409920963731, ; 272: System.IO.Compression.ZipFile.dll => 0x69fd26fcb637f493 => 47
	i64 7654504624184590948, ; 273: System.Net.Http => 0x6a3a4366801b8264 => 66
	i64 7694700312542370399, ; 274: System.Net.Mail => 0x6ac9112a7e2cda5f => 68
	i64 7708790323521193081, ; 275: ms/Microsoft.Maui.Controls.resources.dll => 0x6afb1ff4d1730479 => 318
	i64 7714652370974252055, ; 276: System.Private.CoreLib => 0x6b0ff375198b9c17 => 174
	i64 7725404731275645577, ; 277: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x6b3626ac11ce9289 => 258
	i64 7735176074855944702, ; 278: Microsoft.CSharp => 0x6b58dda848e391fe => 3
	i64 7735352534559001595, ; 279: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 297
	i64 7791074099216502080, ; 280: System.IO.FileSystem.AccessControl.dll => 0x6c1f749d468bcd40 => 49
	i64 7820441508502274321, ; 281: System.Data => 0x6c87ca1e14ff8111 => 26
	i64 7836164640616011524, ; 282: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 225
	i64 7919757340696389605, ; 283: Microsoft.Extensions.Diagnostics.Abstractions => 0x6de8a157378027e5 => 190
	i64 7972383140441761405, ; 284: Microsoft.Extensions.Caching.Abstractions.dll => 0x6ea3983a0b58267d => 179
	i64 8025517457475554965, ; 285: WindowsBase => 0x6f605d9b4786ce95 => 167
	i64 8031450141206250471, ; 286: System.Runtime.Intrinsics.dll => 0x6f757159d9dc03e7 => 110
	i64 8064050204834738623, ; 287: System.Collections.dll => 0x6fe942efa61731bf => 14
	i64 8083354569033831015, ; 288: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 252
	i64 8085230611270010360, ; 289: System.Net.Http.Json.dll => 0x703482674fdd05f8 => 65
	i64 8087206902342787202, ; 290: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 29
	i64 8103644804370223335, ; 291: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 25
	i64 8113615946733131500, ; 292: System.Reflection.Extensions => 0x70995ab73cf916ec => 95
	i64 8167236081217502503, ; 293: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 170
	i64 8185542183669246576, ; 294: System.Collections => 0x7198e33f4794aa70 => 14
	i64 8187640529827139739, ; 295: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 298
	i64 8246048515196606205, ; 296: Microsoft.Maui.Graphics.dll => 0x726fd96f64ee56fd => 206
	i64 8264926008854159966, ; 297: System.Diagnostics.Process.dll => 0x72b2ea6a64a3a25e => 31
	i64 8269874707715456906, ; 298: de/MiviaMaui.resources.dll => 0x72c47f3b225dff8a => 0
	i64 8290740647658429042, ; 299: System.Runtime.Extensions => 0x730ea0b15c929a72 => 105
	i64 8318905602908530212, ; 300: System.ComponentModel.DataAnnotations => 0x7372b092055ea624 => 16
	i64 8368701292315763008, ; 301: System.Security.Cryptography => 0x7423997c6fd56140 => 128
	i64 8398329775253868912, ; 302: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x748cdc6f3097d170 => 235
	i64 8400357532724379117, ; 303: Xamarin.AndroidX.Navigation.UI.dll => 0x749410ab44503ded => 267
	i64 8410671156615598628, ; 304: System.Reflection.Emit.Lightweight.dll => 0x74b8b4daf4b25224 => 93
	i64 8426919725312979251, ; 305: Xamarin.AndroidX.Lifecycle.Process => 0x74f26ed7aa033133 => 256
	i64 8518412311883997971, ; 306: System.Collections.Immutable => 0x76377add7c28e313 => 11
	i64 8563666267364444763, ; 307: System.Private.Uri => 0x76d841191140ca5b => 88
	i64 8598790081731763592, ; 308: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x77550a055fc61d88 => 246
	i64 8599632406834268464, ; 309: CommunityToolkit.Maui => 0x7758081c784b4930 => 175
	i64 8601935802264776013, ; 310: Xamarin.AndroidX.Transition.dll => 0x7760370982b4ed4d => 279
	i64 8614108721271900878, ; 311: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x778b763e14018ace => 322
	i64 8623059219396073920, ; 312: System.Net.Quic.dll => 0x77ab42ac514299c0 => 73
	i64 8626175481042262068, ; 313: Java.Interop => 0x77b654e585b55834 => 170
	i64 8638972117149407195, ; 314: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 3
	i64 8639588376636138208, ; 315: Xamarin.AndroidX.Navigation.Runtime => 0x77e5fbdaa2fda2e0 => 266
	i64 8648495978913578441, ; 316: Microsoft.Win32.Registry.dll => 0x7805a1456889bdc9 => 7
	i64 8677882282824630478, ; 317: pt-BR\Microsoft.Maui.Controls.resources => 0x786e07f5766b00ce => 322
	i64 8684531736582871431, ; 318: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 46
	i64 8725526185868997716, ; 319: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 29
	i64 8816904670177563593, ; 320: Microsoft.Extensions.Diagnostics => 0x7a5bf015646a93c9 => 189
	i64 8941376889969657626, ; 321: System.Xml.XDocument => 0x7c1626e87187471a => 160
	i64 8951477988056063522, ; 322: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0x7c3a09cd9ccf5e22 => 269
	i64 8954753533646919997, ; 323: System.Runtime.Serialization.Json => 0x7c45ace50032d93d => 114
	i64 9031035476476434958, ; 324: Xamarin.KotlinX.Coroutines.Core.dll => 0x7d54aeead9541a0e => 299
	i64 9045785047181495996, ; 325: zh-HK\Microsoft.Maui.Controls.resources => 0x7d891592e3cb0ebc => 332
	i64 9111603110219107042, ; 326: Microsoft.Extensions.Caching.Memory => 0x7e72eac0def44ae2 => 180
	i64 9138683372487561558, ; 327: System.Security.Cryptography.Csp => 0x7ed3201bc3e3d156 => 123
	i64 9312692141327339315, ; 328: Xamarin.AndroidX.ViewPager2 => 0x813d54296a634f33 => 284
	i64 9324707631942237306, ; 329: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 224
	i64 9342122023452561803, ; 330: SixLabors.ImageSharp => 0x81a5e27bd03e518b => 208
	i64 9468215723722196442, ; 331: System.Xml.XPath.XDocument.dll => 0x8365dc09353ac5da => 161
	i64 9554839972845591462, ; 332: System.ServiceModel.Web => 0x84999c54e32a1ba6 => 133
	i64 9575902398040817096, ; 333: Xamarin.Google.Crypto.Tink.Android.dll => 0x84e4707ee708bdc8 => 289
	i64 9584643793929893533, ; 334: System.IO.dll => 0x85037ebfbbd7f69d => 59
	i64 9650158550865574924, ; 335: Microsoft.Extensions.Configuration.Json => 0x85ec4012c28a7c0c => 185
	i64 9659729154652888475, ; 336: System.Text.RegularExpressions => 0x860e407c9991dd9b => 140
	i64 9662334977499516867, ; 337: System.Numerics.dll => 0x8617827802b0cfc3 => 85
	i64 9667360217193089419, ; 338: System.Diagnostics.StackTrace => 0x86295ce5cd89898b => 32
	i64 9678050649315576968, ; 339: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 236
	i64 9702891218465930390, ; 340: System.Collections.NonGeneric.dll => 0x86a79827b2eb3c96 => 12
	i64 9780093022148426479, ; 341: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x87b9dec9576efaef => 286
	i64 9808709177481450983, ; 342: Mono.Android.dll => 0x881f890734e555e7 => 173
	i64 9825649861376906464, ; 343: Xamarin.AndroidX.Concurrent.Futures => 0x885bb87d8abc94e0 => 233
	i64 9834056768316610435, ; 344: System.Transactions.dll => 0x8879968718899783 => 152
	i64 9836529246295212050, ; 345: System.Reflection.Metadata => 0x88825f3bbc2ac012 => 96
	i64 9875200773399460291, ; 346: Xamarin.GooglePlayServices.Base.dll => 0x890bc2c8482339c3 => 292
	i64 9907349773706910547, ; 347: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x897dfa20b758db53 => 246
	i64 9933555792566666578, ; 348: System.Linq.Queryable.dll => 0x89db145cf475c552 => 62
	i64 9956195530459977388, ; 349: Microsoft.Maui => 0x8a2b8315b36616ac => 204
	i64 9974604633896246661, ; 350: System.Xml.Serialization.dll => 0x8a6cea111a59dd85 => 159
	i64 9991543690424095600, ; 351: es/Microsoft.Maui.Controls.resources.dll => 0x8aa9180c89861370 => 307
	i64 10017511394021241210, ; 352: Microsoft.Extensions.Logging.Debug => 0x8b055989ae10717a => 197
	i64 10038780035334861115, ; 353: System.Net.Http.dll => 0x8b50e941206af13b => 66
	i64 10051358222726253779, ; 354: System.Private.Xml => 0x8b7d990c97ccccd3 => 90
	i64 10078727084704864206, ; 355: System.Net.WebSockets.Client => 0x8bded4e257f117ce => 81
	i64 10089571585547156312, ; 356: System.IO.FileSystem.AccessControl => 0x8c055be67469bb58 => 49
	i64 10092835686693276772, ; 357: Microsoft.Maui.Controls => 0x8c10f49539bd0c64 => 202
	i64 10105485790837105934, ; 358: System.Threading.Tasks.Parallel => 0x8c3de5c91d9a650e => 145
	i64 10143853363526200146, ; 359: da\Microsoft.Maui.Controls.resources => 0x8cc634e3c2a16b52 => 304
	i64 10205853378024263619, ; 360: Microsoft.Extensions.Configuration.Binder => 0x8da279930adb4fc3 => 183
	i64 10229024438826829339, ; 361: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 240
	i64 10236703004850800690, ; 362: System.Net.ServicePoint.dll => 0x8e101325834e4832 => 76
	i64 10245369515835430794, ; 363: System.Reflection.Emit.Lightweight => 0x8e2edd4ad7fc978a => 93
	i64 10321854143672141184, ; 364: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 296
	i64 10360651442923773544, ; 365: System.Text.Encoding => 0x8fc86d98211c1e68 => 137
	i64 10364469296367737616, ; 366: System.Reflection.Emit.ILGeneration.dll => 0x8fd5fde967711b10 => 92
	i64 10376576884623852283, ; 367: Xamarin.AndroidX.Tracing.Tracing => 0x900101b2f888c2fb => 278
	i64 10406448008575299332, ; 368: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 300
	i64 10418585070879608571, ; 369: JpegLibrary => 0x90963fec03f78efb => 178
	i64 10430153318873392755, ; 370: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 237
	i64 10506226065143327199, ; 371: ca\Microsoft.Maui.Controls.resources => 0x91cd9cf11ed169df => 302
	i64 10546663366131771576, ; 372: System.Runtime.Serialization.Json.dll => 0x925d4673efe8e8b8 => 114
	i64 10566960649245365243, ; 373: System.Globalization.dll => 0x92a562b96dcd13fb => 44
	i64 10595762989148858956, ; 374: System.Xml.XPath.XDocument => 0x930bb64cc472ea4c => 161
	i64 10670374202010151210, ; 375: Microsoft.Win32.Primitives.dll => 0x9414c8cd7b4ea92a => 6
	i64 10714184849103829812, ; 376: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 105
	i64 10785150219063592792, ; 377: System.Net.Primitives => 0x95ac8cfb68830758 => 72
	i64 10809043855025277762, ; 378: Microsoft.Extensions.Options.ConfigurationExtensions => 0x9601701e0c668b42 => 199
	i64 10822644899632537592, ; 379: System.Linq.Queryable => 0x9631c23204ca5ff8 => 62
	i64 10830817578243619689, ; 380: System.Formats.Tar => 0x964ecb340a447b69 => 41
	i64 10847732767863316357, ; 381: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 226
	i64 10880838204485145808, ; 382: CommunityToolkit.Maui.dll => 0x970080b2a4d614d0 => 175
	i64 10899834349646441345, ; 383: System.Web => 0x9743fd975946eb81 => 155
	i64 10943875058216066601, ; 384: System.IO.UnmanagedMemoryStream.dll => 0x97e07461df39de29 => 58
	i64 10964653383833615866, ; 385: System.Diagnostics.Tracing => 0x982a4628ccaffdfa => 36
	i64 11002576679268595294, ; 386: Microsoft.Extensions.Logging.Abstractions => 0x98b1013215cd365e => 196
	i64 11009005086950030778, ; 387: Microsoft.Maui.dll => 0x98c7d7cc621ffdba => 204
	i64 11019817191295005410, ; 388: Xamarin.AndroidX.Annotation.Jvm.dll => 0x98ee415998e1b2e2 => 223
	i64 11023048688141570732, ; 389: System.Core => 0x98f9bc61168392ac => 23
	i64 11037814507248023548, ; 390: System.Xml => 0x992e31d0412bf7fc => 165
	i64 11071824625609515081, ; 391: Xamarin.Google.ErrorProne.Annotations => 0x99a705d600e0a049 => 290
	i64 11103970607964515343, ; 392: hu\Microsoft.Maui.Controls.resources => 0x9a193a6fc41a6c0f => 313
	i64 11136029745144976707, ; 393: Jsr305Binding.dll => 0x9a8b200d4f8cd543 => 288
	i64 11162124722117608902, ; 394: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 283
	i64 11188319605227840848, ; 395: System.Threading.Overlapped => 0x9b44e5671724e550 => 142
	i64 11220793807500858938, ; 396: ja\Microsoft.Maui.Controls.resources => 0x9bb8448481fdd63a => 316
	i64 11226290749488709958, ; 397: Microsoft.Extensions.Options.dll => 0x9bcbcbf50c874146 => 198
	i64 11235648312900863002, ; 398: System.Reflection.DispatchProxy.dll => 0x9bed0a9c8fac441a => 91
	i64 11299661109949763898, ; 399: Xamarin.AndroidX.Collection.Jvm => 0x9cd075e94cda113a => 231
	i64 11329751333533450475, ; 400: System.Threading.Timer.dll => 0x9d3b5ccf6cc500eb => 149
	i64 11340910727871153756, ; 401: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 239
	i64 11347436699239206956, ; 402: System.Xml.XmlSerializer.dll => 0x9d7a318e8162502c => 164
	i64 11392833485892708388, ; 403: Xamarin.AndroidX.Print.dll => 0x9e1b79b18fcf6824 => 268
	i64 11432101114902388181, ; 404: System.AppContext => 0x9ea6fb64e61a9dd5 => 8
	i64 11446671985764974897, ; 405: Mono.Android.Export => 0x9edabf8623efc131 => 171
	i64 11448276831755070604, ; 406: System.Diagnostics.TextWriterTraceListener => 0x9ee0731f77186c8c => 33
	i64 11485890710487134646, ; 407: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 109
	i64 11508496261504176197, ; 408: Xamarin.AndroidX.Fragment.Ktx.dll => 0x9fb664600dde1045 => 249
	i64 11518296021396496455, ; 409: id\Microsoft.Maui.Controls.resources => 0x9fd9353475222047 => 314
	i64 11529969570048099689, ; 410: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 283
	i64 11530571088791430846, ; 411: Microsoft.Extensions.Logging => 0xa004d1504ccd66be => 195
	i64 11580057168383206117, ; 412: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 221
	i64 11591352189662810718, ; 413: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0xa0dcc167234c525e => 276
	i64 11597940890313164233, ; 414: netstandard => 0xa0f429ca8d1805c9 => 169
	i64 11672361001936329215, ; 415: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 250
	i64 11692977985522001935, ; 416: System.Threading.Overlapped.dll => 0xa245cd869980680f => 142
	i64 11705530742807338875, ; 417: he/Microsoft.Maui.Controls.resources.dll => 0xa272663128721f7b => 310
	i64 11707554492040141440, ; 418: System.Linq.Parallel.dll => 0xa27996c7fe94da80 => 61
	i64 11739066727115742305, ; 419: SQLite-net.dll => 0xa2e98afdf8575c61 => 209
	i64 11743665907891708234, ; 420: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 146
	i64 11806260347154423189, ; 421: SQLite-net => 0xa3d8433bc5eb5d95 => 209
	i64 11991047634523762324, ; 422: System.Net => 0xa668c24ad493ae94 => 83
	i64 12040886584167504988, ; 423: System.Net.ServicePoint => 0xa719d28d8e121c5c => 76
	i64 12048689113179125827, ; 424: Microsoft.Extensions.FileProviders.Physical => 0xa7358ae968287843 => 192
	i64 12058074296353848905, ; 425: Microsoft.Extensions.FileSystemGlobbing.dll => 0xa756e2afa5707e49 => 193
	i64 12063623837170009990, ; 426: System.Security => 0xa76a99f6ce740786 => 132
	i64 12096697103934194533, ; 427: System.Diagnostics.Contracts => 0xa7e019eccb7e8365 => 27
	i64 12102847907131387746, ; 428: System.Buffers => 0xa7f5f40c43256f62 => 9
	i64 12123043025855404482, ; 429: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 95
	i64 12137774235383566651, ; 430: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 280
	i64 12145679461940342714, ; 431: System.Text.Json => 0xa88e1f1ebcb62fba => 139
	i64 12191646537372739477, ; 432: Xamarin.Android.Glide.dll => 0xa9316dee7f392795 => 215
	i64 12201331334810686224, ; 433: System.Runtime.Serialization.Primitives.dll => 0xa953d6341e3bd310 => 115
	i64 12269460666702402136, ; 434: System.Collections.Immutable.dll => 0xaa45e178506c9258 => 11
	i64 12279246230491828964, ; 435: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 213
	i64 12332222936682028543, ; 436: System.Runtime.Handles => 0xab24db6c07db5dff => 106
	i64 12341818387765915815, ; 437: CommunityToolkit.Maui.Core.dll => 0xab46f26f152bf0a7 => 176
	i64 12375446203996702057, ; 438: System.Configuration.dll => 0xabbe6ac12e2e0569 => 21
	i64 12451044538927396471, ; 439: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 248
	i64 12466513435562512481, ; 440: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 262
	i64 12475113361194491050, ; 441: _Microsoft.Android.Resource.Designer.dll => 0xad2081818aba1caa => 335
	i64 12487638416075308985, ; 442: Xamarin.AndroidX.DocumentFile.dll => 0xad4d00fa21b0bfb9 => 242
	i64 12517810545449516888, ; 443: System.Diagnostics.TraceSource.dll => 0xadb8325e6f283f58 => 35
	i64 12538491095302438457, ; 444: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 229
	i64 12550732019250633519, ; 445: System.IO.Compression => 0xae2d28465e8e1b2f => 48
	i64 12681088699309157496, ; 446: it/Microsoft.Maui.Controls.resources.dll => 0xaffc46fc178aec78 => 315
	i64 12699999919562409296, ; 447: System.Diagnostics.StackTrace.dll => 0xb03f76a3ad01c550 => 32
	i64 12700543734426720211, ; 448: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 230
	i64 12708238894395270091, ; 449: System.IO => 0xb05cbbf17d3ba3cb => 59
	i64 12708922737231849740, ; 450: System.Text.Encoding.Extensions => 0xb05f29e50e96e90c => 136
	i64 12717050818822477433, ; 451: System.Runtime.Serialization.Xml.dll => 0xb07c0a5786811679 => 116
	i64 12753841065332862057, ; 452: Xamarin.AndroidX.Window => 0xb0febee04cf46c69 => 285
	i64 12823819093633476069, ; 453: th/Microsoft.Maui.Controls.resources.dll => 0xb1f75b85abe525e5 => 328
	i64 12835242264250840079, ; 454: System.IO.Pipes => 0xb21ff0d5d6c0740f => 57
	i64 12843321153144804894, ; 455: Microsoft.Extensions.Primitives => 0xb23ca48abd74d61e => 200
	i64 12843770487262409629, ; 456: System.AppContext.dll => 0xb23e3d357debf39d => 8
	i64 12859557719246324186, ; 457: System.Net.WebHeaderCollection.dll => 0xb276539ce04f41da => 79
	i64 12982280885948128408, ; 458: Xamarin.AndroidX.CustomView.PoolingContainer => 0xb42a53aec5481c98 => 241
	i64 13068258254871114833, ; 459: System.Runtime.Serialization.Formatters.dll => 0xb55bc7a4eaa8b451 => 113
	i64 13129914918964716986, ; 460: Xamarin.AndroidX.Emoji2.dll => 0xb636d40db3fe65ba => 245
	i64 13173818576982874404, ; 461: System.Runtime.CompilerServices.VisualC.dll => 0xb6d2ce32a8819924 => 104
	i64 13221551921002590604, ; 462: ca/Microsoft.Maui.Controls.resources.dll => 0xb77c636bdebe318c => 302
	i64 13222659110913276082, ; 463: ja/Microsoft.Maui.Controls.resources.dll => 0xb78052679c1178b2 => 316
	i64 13343850469010654401, ; 464: Mono.Android.Runtime.dll => 0xb92ee14d854f44c1 => 172
	i64 13370592475155966277, ; 465: System.Runtime.Serialization => 0xb98de304062ea945 => 117
	i64 13381594904270902445, ; 466: he\Microsoft.Maui.Controls.resources => 0xb9b4f9aaad3e94ad => 310
	i64 13401370062847626945, ; 467: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 280
	i64 13404347523447273790, ; 468: Xamarin.AndroidX.ConstraintLayout.Core => 0xba05cf0da4f6393e => 235
	i64 13431476299110033919, ; 469: System.Net.WebClient => 0xba663087f18829ff => 78
	i64 13442656863351652662, ; 470: pl/MiviaMaui.resources.dll => 0xba8de931e98ac136 => 1
	i64 13454009404024712428, ; 471: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 291
	i64 13463706743370286408, ; 472: System.Private.DataContractSerialization.dll => 0xbad8b1f3069e0548 => 87
	i64 13465488254036897740, ; 473: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 297
	i64 13467053111158216594, ; 474: uk/Microsoft.Maui.Controls.resources.dll => 0xbae49573fde79792 => 330
	i64 13491513212026656886, ; 475: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 227
	i64 13540124433173649601, ; 476: vi\Microsoft.Maui.Controls.resources => 0xbbe82f6eede718c1 => 331
	i64 13545416393490209236, ; 477: id/Microsoft.Maui.Controls.resources.dll => 0xbbfafc7174bc99d4 => 314
	i64 13550417756503177631, ; 478: Microsoft.Extensions.FileProviders.Abstractions.dll => 0xbc0cc1280684799f => 191
	i64 13572454107664307259, ; 479: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 270
	i64 13578472628727169633, ; 480: System.Xml.XPath => 0xbc706ce9fba5c261 => 162
	i64 13580399111273692417, ; 481: Microsoft.VisualBasic.Core.dll => 0xbc77450a277fbd01 => 4
	i64 13621154251410165619, ; 482: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0xbd080f9faa1acf73 => 241
	i64 13647894001087880694, ; 483: System.Data.dll => 0xbd670f48cb071df6 => 26
	i64 13675589307506966157, ; 484: Xamarin.AndroidX.Activity.Ktx => 0xbdc97404d0153e8d => 220
	i64 13702626353344114072, ; 485: System.Diagnostics.Tools.dll => 0xbe29821198fb6d98 => 34
	i64 13710614125866346983, ; 486: System.Security.AccessControl.dll => 0xbe45e2e7d0b769e7 => 119
	i64 13713329104121190199, ; 487: System.Dynamic.Runtime => 0xbe4f8829f32b5737 => 39
	i64 13717397318615465333, ; 488: System.ComponentModel.Primitives.dll => 0xbe5dfc2ef2f87d75 => 18
	i64 13755568601956062840, ; 489: fr/Microsoft.Maui.Controls.resources.dll => 0xbee598c36b1b9678 => 309
	i64 13768883594457632599, ; 490: System.IO.IsolatedStorage => 0xbf14e6adb159cf57 => 54
	i64 13814445057219246765, ; 491: hr/Microsoft.Maui.Controls.resources.dll => 0xbfb6c49664b43aad => 312
	i64 13881769479078963060, ; 492: System.Console.dll => 0xc0a5f3cade5c6774 => 22
	i64 13911222732217019342, ; 493: System.Security.Cryptography.OpenSsl.dll => 0xc10e975ec1226bce => 125
	i64 13928444506500929300, ; 494: System.Windows.dll => 0xc14bc67b8bba9714 => 156
	i64 13959074834287824816, ; 495: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 248
	i64 14075334701871371868, ; 496: System.ServiceModel.Web.dll => 0xc355a25647c5965c => 133
	i64 14100563506285742564, ; 497: da/Microsoft.Maui.Controls.resources.dll => 0xc3af43cd0cff89e4 => 304
	i64 14102401948926751915, ; 498: JpegLibrary.dll => 0xc3b5cbdad58e34ab => 178
	i64 14124974489674258913, ; 499: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 229
	i64 14125464355221830302, ; 500: System.Threading.dll => 0xc407bafdbc707a9e => 150
	i64 14178052285788134900, ; 501: Xamarin.Android.Glide.Annotations.dll => 0xc4c28f6f75511df4 => 216
	i64 14212104595480609394, ; 502: System.Security.Cryptography.Cng.dll => 0xc53b89d4a4518272 => 122
	i64 14220608275227875801, ; 503: System.Diagnostics.FileVersionInfo.dll => 0xc559bfe1def019d9 => 30
	i64 14226382999226559092, ; 504: System.ServiceProcess => 0xc56e43f6938e2a74 => 134
	i64 14232023429000439693, ; 505: System.Resources.Writer.dll => 0xc5824de7789ba78d => 102
	i64 14254574811015963973, ; 506: System.Text.Encoding.Extensions.dll => 0xc5d26c4442d66545 => 136
	i64 14261073672896646636, ; 507: Xamarin.AndroidX.Print => 0xc5e982f274ae0dec => 268
	i64 14298246716367104064, ; 508: System.Web.dll => 0xc66d93a217f4e840 => 155
	i64 14327695147300244862, ; 509: System.Reflection.dll => 0xc6d632d338eb4d7e => 99
	i64 14327709162229390963, ; 510: System.Security.Cryptography.X509Certificates => 0xc6d63f9253cade73 => 127
	i64 14331727281556788554, ; 511: Xamarin.Android.Glide.DiskLruCache.dll => 0xc6e48607a2f7954a => 217
	i64 14346402571976470310, ; 512: System.Net.Ping.dll => 0xc718a920f3686f26 => 71
	i64 14461014870687870182, ; 513: System.Net.Requests.dll => 0xc8afd8683afdece6 => 74
	i64 14464374589798375073, ; 514: ru\Microsoft.Maui.Controls.resources => 0xc8bbc80dcb1e5ea1 => 325
	i64 14486659737292545672, ; 515: Xamarin.AndroidX.Lifecycle.LiveData => 0xc90af44707469e88 => 253
	i64 14495724990987328804, ; 516: Xamarin.AndroidX.ResourceInspection.Annotation => 0xc92b2913e18d5d24 => 271
	i64 14522721392235705434, ; 517: el/Microsoft.Maui.Controls.resources.dll => 0xc98b12295c2cf45a => 306
	i64 14551742072151931844, ; 518: System.Text.Encodings.Web.dll => 0xc9f22c50f1b8fbc4 => 138
	i64 14556034074661724008, ; 519: CommunityToolkit.Maui.Core => 0xca016bdea6b19f68 => 176
	i64 14561513370130550166, ; 520: System.Security.Cryptography.Primitives.dll => 0xca14e3428abb8d96 => 126
	i64 14574160591280636898, ; 521: System.Net.Quic => 0xca41d1d72ec783e2 => 73
	i64 14622043554576106986, ; 522: System.Runtime.Serialization.Formatters => 0xcaebef2458cc85ea => 113
	i64 14644440854989303794, ; 523: Xamarin.AndroidX.LocalBroadcastManager.dll => 0xcb3b815e37daeff2 => 263
	i64 14669215534098758659, ; 524: Microsoft.Extensions.DependencyInjection.dll => 0xcb9385ceb3993c03 => 187
	i64 14678510994762383812, ; 525: Xamarin.GooglePlayServices.Location.dll => 0xcbb48bfaca7a41c4 => 294
	i64 14690985099581930927, ; 526: System.Web.HttpUtility => 0xcbe0dd1ca5233daf => 154
	i64 14705122255218365489, ; 527: ko\Microsoft.Maui.Controls.resources => 0xcc1316c7b0fb5431 => 317
	i64 14744092281598614090, ; 528: zh-Hans\Microsoft.Maui.Controls.resources => 0xcc9d89d004439a4a => 333
	i64 14792063746108907174, ; 529: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 291
	i64 14832630590065248058, ; 530: System.Security.Claims => 0xcdd816ef5d6e873a => 120
	i64 14852515768018889994, ; 531: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 239
	i64 14889905118082851278, ; 532: GoogleGson.dll => 0xcea391d0969961ce => 177
	i64 14892012299694389861, ; 533: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xceab0e490a083a65 => 334
	i64 14904040806490515477, ; 534: ar\Microsoft.Maui.Controls.resources => 0xced5ca2604cb2815 => 301
	i64 14912225920358050525, ; 535: System.Security.Principal.Windows => 0xcef2de7759506add => 129
	i64 14935719434541007538, ; 536: System.Text.Encoding.CodePages.dll => 0xcf4655b160b702b2 => 135
	i64 14954917835170835695, ; 537: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xcf8a8a895a82ecef => 188
	i64 14984936317414011727, ; 538: System.Net.WebHeaderCollection => 0xcff5302fe54ff34f => 79
	i64 14987728460634540364, ; 539: System.IO.Compression.dll => 0xcfff1ba06622494c => 48
	i64 14988210264188246988, ; 540: Xamarin.AndroidX.DocumentFile => 0xd000d1d307cddbcc => 242
	i64 15004680457737980385, ; 541: Microsoft.Extensions.Configuration.UserSecrets => 0xd03b5560cbb7c9e1 => 186
	i64 15015154896917945444, ; 542: System.Net.Security.dll => 0xd0608bd33642dc64 => 75
	i64 15024878362326791334, ; 543: System.Net.Http.Json => 0xd0831743ebf0f4a6 => 65
	i64 15051741671811457419, ; 544: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0xd0e2874d8f44218b => 190
	i64 15071021337266399595, ; 545: System.Resources.Reader.dll => 0xd127060e7a18a96b => 100
	i64 15076659072870671916, ; 546: System.ObjectModel.dll => 0xd13b0d8c1620662c => 86
	i64 15111608613780139878, ; 547: ms\Microsoft.Maui.Controls.resources => 0xd1b737f831192f66 => 318
	i64 15115185479366240210, ; 548: System.IO.Compression.Brotli.dll => 0xd1c3ed1c1bc467d2 => 45
	i64 15133485256822086103, ; 549: System.Linq.dll => 0xd204f0a9127dd9d7 => 63
	i64 15150743910298169673, ; 550: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xd2424150783c3149 => 269
	i64 15227001540531775957, ; 551: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd3512d3999b8e9d5 => 182
	i64 15234786388537674379, ; 552: System.Dynamic.Runtime.dll => 0xd36cd580c5be8a8b => 39
	i64 15250465174479574862, ; 553: System.Globalization.Calendars.dll => 0xd3a489469852174e => 42
	i64 15272359115529052076, ; 554: Xamarin.AndroidX.Collection.Ktx => 0xd3f251b2fb4edfac => 232
	i64 15279429628684179188, ; 555: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 298
	i64 15299439993936780255, ; 556: System.Xml.XPath.dll => 0xd452879d55019bdf => 162
	i64 15338463749992804988, ; 557: System.Resources.Reader => 0xd4dd2b839286f27c => 100
	i64 15370334346939861994, ; 558: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 237
	i64 15391712275433856905, ; 559: Microsoft.Extensions.DependencyInjection.Abstractions => 0xd59a58c406411f89 => 188
	i64 15526743539506359484, ; 560: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 137
	i64 15527772828719725935, ; 561: System.Console => 0xd77dbb1e38cd3d6f => 22
	i64 15530465045505749832, ; 562: System.Net.HttpListener.dll => 0xd7874bacc9fdb348 => 67
	i64 15536481058354060254, ; 563: de\Microsoft.Maui.Controls.resources => 0xd79cab34eec75bde => 305
	i64 15541854775306130054, ; 564: System.Security.Cryptography.X509Certificates.dll => 0xd7afc292e8d49286 => 127
	i64 15557562860424774966, ; 565: System.Net.Sockets => 0xd7e790fe7a6dc536 => 77
	i64 15582737692548360875, ; 566: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xd841015ed86f6aab => 261
	i64 15609085926864131306, ; 567: System.dll => 0xd89e9cf3334914ea => 166
	i64 15661133872274321916, ; 568: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 158
	i64 15664356999916475676, ; 569: de/Microsoft.Maui.Controls.resources.dll => 0xd962f9b2b6ecd51c => 305
	i64 15710114879900314733, ; 570: Microsoft.Win32.Registry => 0xda058a3f5d096c6d => 7
	i64 15743187114543869802, ; 571: hu/Microsoft.Maui.Controls.resources.dll => 0xda7b09450ae4ef6a => 313
	i64 15755368083429170162, ; 572: System.IO.FileSystem.Primitives => 0xdaa64fcbde529bf2 => 51
	i64 15777549416145007739, ; 573: Xamarin.AndroidX.SlidingPaneLayout.dll => 0xdaf51d99d77eb47b => 275
	i64 15783653065526199428, ; 574: el\Microsoft.Maui.Controls.resources => 0xdb0accd674b1c484 => 306
	i64 15817206913877585035, ; 575: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 146
	i64 15827202283623377193, ; 576: Microsoft.Extensions.Configuration.Json.dll => 0xdba5849eef9f6929 => 185
	i64 15847085070278954535, ; 577: System.Threading.Channels.dll => 0xdbec27e8f35f8e27 => 141
	i64 15885744048853936810, ; 578: System.Resources.Writer => 0xdc75800bd0b6eaaa => 102
	i64 15928521404965645318, ; 579: Microsoft.Maui.Controls.Compatibility => 0xdd0d79d32c2eec06 => 201
	i64 15930129725311349754, ; 580: Xamarin.GooglePlayServices.Tasks.dll => 0xdd1330956f12f3fa => 295
	i64 15934062614519587357, ; 581: System.Security.Cryptography.OpenSsl => 0xdd2129868f45a21d => 125
	i64 15937190497610202713, ; 582: System.Security.Cryptography.Cng => 0xdd2c465197c97e59 => 122
	i64 15963349826457351533, ; 583: System.Threading.Tasks.Extensions => 0xdd893616f748b56d => 144
	i64 15971679995444160383, ; 584: System.Formats.Tar.dll => 0xdda6ce5592a9677f => 41
	i64 16018552496348375205, ; 585: System.Net.NetworkInformation.dll => 0xde4d54a020caa8a5 => 70
	i64 16054465462676478687, ; 586: System.Globalization.Extensions => 0xdecceb47319bdadf => 43
	i64 16154507427712707110, ; 587: System => 0xe03056ea4e39aa26 => 166
	i64 16219561732052121626, ; 588: System.Net.Security => 0xe1177575db7c781a => 75
	i64 16288847719894691167, ; 589: nb\Microsoft.Maui.Controls.resources => 0xe20d9cb300c12d5f => 319
	i64 16315482530584035869, ; 590: WindowsBase.dll => 0xe26c3ceb1e8d821d => 167
	i64 16321164108206115771, ; 591: Microsoft.Extensions.Logging.Abstractions.dll => 0xe2806c487e7b0bbb => 196
	i64 16337011941688632206, ; 592: System.Security.Principal.Windows.dll => 0xe2b8b9cdc3aa638e => 129
	i64 16361933716545543812, ; 593: Xamarin.AndroidX.ExifInterface.dll => 0xe3114406a52f1e84 => 247
	i64 16454459195343277943, ; 594: System.Net.NetworkInformation => 0xe459fb756d988f77 => 70
	i64 16496768397145114574, ; 595: Mono.Android.Export.dll => 0xe4f04b741db987ce => 171
	i64 16558262036769511634, ; 596: Microsoft.Extensions.Http => 0xe5cac397cf7b98d2 => 194
	i64 16589693266713801121, ; 597: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xe63a6e214f2a71a1 => 260
	i64 16621146507174665210, ; 598: Xamarin.AndroidX.ConstraintLayout => 0xe6aa2caf87dedbfa => 234
	i64 16649148416072044166, ; 599: Microsoft.Maui.Graphics => 0xe70da84600bb4e86 => 206
	i64 16677317093839702854, ; 600: Xamarin.AndroidX.Navigation.UI => 0xe771bb8960dd8b46 => 267
	i64 16702652415771857902, ; 601: System.ValueTuple => 0xe7cbbde0b0e6d3ee => 153
	i64 16709499819875633724, ; 602: System.IO.Compression.ZipFile => 0xe7e4118e32240a3c => 47
	i64 16737807731308835127, ; 603: System.Runtime.Intrinsics => 0xe848a3736f733137 => 110
	i64 16755018182064898362, ; 604: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 211
	i64 16758309481308491337, ; 605: System.IO.FileSystem.DriveInfo => 0xe89179af15740e49 => 50
	i64 16762783179241323229, ; 606: System.Reflection.TypeExtensions => 0xe8a15e7d0d927add => 98
	i64 16765015072123548030, ; 607: System.Diagnostics.TextWriterTraceListener.dll => 0xe8a94c621bfe717e => 33
	i64 16822611501064131242, ; 608: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 25
	i64 16833383113903931215, ; 609: mscorlib => 0xe99c30c1484d7f4f => 168
	i64 16856067890322379635, ; 610: System.Data.Common.dll => 0xe9ecc87060889373 => 24
	i64 16890310621557459193, ; 611: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 140
	i64 16933958494752847024, ; 612: System.Net.WebProxy.dll => 0xeb018187f0f3b4b0 => 80
	i64 16942731696432749159, ; 613: sk\Microsoft.Maui.Controls.resources => 0xeb20acb622a01a67 => 326
	i64 16977952268158210142, ; 614: System.IO.Pipes.AccessControl => 0xeb9dcda2851b905e => 56
	i64 16989020923549080504, ; 615: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xebc52084add25bb8 => 260
	i64 16998075588627545693, ; 616: Xamarin.AndroidX.Navigation.Fragment => 0xebe54bb02d623e5d => 265
	i64 17008137082415910100, ; 617: System.Collections.NonGeneric => 0xec090a90408c8cd4 => 12
	i64 17024911836938395553, ; 618: Xamarin.AndroidX.Annotation.Experimental.dll => 0xec44a31d250e5fa1 => 222
	i64 17031351772568316411, ; 619: Xamarin.AndroidX.Navigation.Common.dll => 0xec5b843380a769fb => 264
	i64 17037200463775726619, ; 620: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xec704b8e0a78fc1b => 251
	i64 17047433665992082296, ; 621: Microsoft.Extensions.Configuration.FileExtensions => 0xec94a699197e4378 => 184
	i64 17062143951396181894, ; 622: System.ComponentModel.Primitives => 0xecc8e986518c9786 => 18
	i64 17071469805149661853, ; 623: Microsoft.Extensions.Configuration.UserSecrets.dll => 0xecea0b56d9c0aa9d => 186
	i64 17089008752050867324, ; 624: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xed285aeb25888c7c => 333
	i64 17118171214553292978, ; 625: System.Threading.Channels => 0xed8ff6060fc420b2 => 141
	i64 17187273293601214786, ; 626: System.ComponentModel.Annotations.dll => 0xee8575ff9aa89142 => 15
	i64 17201328579425343169, ; 627: System.ComponentModel.EventBasedAsync => 0xeeb76534d96c16c1 => 17
	i64 17202182880784296190, ; 628: System.Security.Cryptography.Encoding.dll => 0xeeba6e30627428fe => 124
	i64 17205988430934219272, ; 629: Microsoft.Extensions.FileSystemGlobbing => 0xeec7f35113509a08 => 193
	i64 17230721278011714856, ; 630: System.Private.Xml.Linq => 0xef1fd1b5c7a72d28 => 89
	i64 17234219099804750107, ; 631: System.Transactions.Local.dll => 0xef2c3ef5e11d511b => 151
	i64 17260702271250283638, ; 632: System.Data.Common => 0xef8a5543bba6bc76 => 24
	i64 17333249706306540043, ; 633: System.Diagnostics.Tracing.dll => 0xf08c12c5bb8b920b => 36
	i64 17338386382517543202, ; 634: System.Net.WebSockets.Client.dll => 0xf09e528d5c6da122 => 81
	i64 17342750010158924305, ; 635: hi\Microsoft.Maui.Controls.resources => 0xf0add33f97ecc211 => 311
	i64 17360349973592121190, ; 636: Xamarin.Google.Crypto.Tink.Android => 0xf0ec5a52686b9f66 => 289
	i64 17438153253682247751, ; 637: sk/Microsoft.Maui.Controls.resources.dll => 0xf200c3fe308d7847 => 326
	i64 17470386307322966175, ; 638: System.Threading.Timer => 0xf27347c8d0d5709f => 149
	i64 17509662556995089465, ; 639: System.Net.WebSockets.dll => 0xf2fed1534ea67439 => 82
	i64 17514990004910432069, ; 640: fr\Microsoft.Maui.Controls.resources => 0xf311be9c6f341f45 => 309
	i64 17522591619082469157, ; 641: GoogleGson => 0xf32cc03d27a5bf25 => 177
	i64 17590473451926037903, ; 642: Xamarin.Android.Glide => 0xf41dea67fcfda58f => 215
	i64 17623389608345532001, ; 643: pl\Microsoft.Maui.Controls.resources => 0xf492db79dfbef661 => 321
	i64 17627500474728259406, ; 644: System.Globalization => 0xf4a176498a351f4e => 44
	i64 17685921127322830888, ; 645: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 28
	i64 17702523067201099846, ; 646: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xf5abfef008ae1846 => 332
	i64 17704177640604968747, ; 647: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 262
	i64 17710060891934109755, ; 648: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 259
	i64 17712670374920797664, ; 649: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 109
	i64 17777860260071588075, ; 650: System.Runtime.Numerics.dll => 0xf6b7a5b72419c0eb => 112
	i64 17838668724098252521, ; 651: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 9
	i64 17891337867145587222, ; 652: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 296
	i64 17928294245072900555, ; 653: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 46
	i64 17986907704309214542, ; 654: Xamarin.GooglePlayServices.Basement.dll => 0xf99e554223166d4e => 293
	i64 17992315986609351877, ; 655: System.Xml.XmlDocument.dll => 0xf9b18c0ffc6eacc5 => 163
	i64 18017743553296241350, ; 656: Microsoft.Extensions.Caching.Abstractions => 0xfa0be24cb44e92c6 => 179
	i64 18025913125965088385, ; 657: System.Threading => 0xfa28e87b91334681 => 150
	i64 18099568558057551825, ; 658: nl/Microsoft.Maui.Controls.resources.dll => 0xfb2e95b53ad977d1 => 320
	i64 18116111925905154859, ; 659: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 227
	i64 18121036031235206392, ; 660: Xamarin.AndroidX.Navigation.Common => 0xfb7ada42d3d42cf8 => 264
	i64 18146411883821974900, ; 661: System.Formats.Asn1.dll => 0xfbd50176eb22c574 => 40
	i64 18146811631844267958, ; 662: System.ComponentModel.EventBasedAsync.dll => 0xfbd66d08820117b6 => 17
	i64 18225059387460068507, ; 663: System.Threading.ThreadPool.dll => 0xfcec6af3cff4a49b => 148
	i64 18245806341561545090, ; 664: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 10
	i64 18260797123374478311, ; 665: Xamarin.AndroidX.Emoji2 => 0xfd6b623bde35f3e7 => 245
	i64 18305135509493619199, ; 666: Xamarin.AndroidX.Navigation.Runtime.dll => 0xfe08e7c2d8c199ff => 266
	i64 18318849532986632368, ; 667: System.Security.dll => 0xfe39a097c37fa8b0 => 132
	i64 18324163916253801303, ; 668: it\Microsoft.Maui.Controls.resources => 0xfe4c81ff0a56ab57 => 315
	i64 18370042311372477656, ; 669: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 212
	i64 18380184030268848184, ; 670: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 282
	i64 18439108438687598470 ; 671: System.Reflection.Metadata.dll => 0xffe4df6e2ee1c786 => 96
], align 8

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [672 x i32] [
	i32 244, ; 0
	i32 207, ; 1
	i32 200, ; 2
	i32 173, ; 3
	i32 205, ; 4
	i32 60, ; 5
	i32 230, ; 6
	i32 153, ; 7
	i32 272, ; 8
	i32 231, ; 9
	i32 275, ; 10
	i32 238, ; 11
	i32 134, ; 12
	i32 199, ; 13
	i32 58, ; 14
	i32 274, ; 15
	i32 294, ; 16
	i32 308, ; 17
	i32 97, ; 18
	i32 257, ; 19
	i32 184, ; 20
	i32 131, ; 21
	i32 183, ; 22
	i32 292, ; 23
	i32 147, ; 24
	i32 232, ; 25
	i32 20, ; 26
	i32 311, ; 27
	i32 212, ; 28
	i32 243, ; 29
	i32 258, ; 30
	i32 152, ; 31
	i32 106, ; 32
	i32 97, ; 33
	i32 287, ; 34
	i32 319, ; 35
	i32 38, ; 36
	i32 211, ; 37
	i32 30, ; 38
	i32 226, ; 39
	i32 265, ; 40
	i32 52, ; 41
	i32 117, ; 42
	i32 72, ; 43
	i32 202, ; 44
	i32 67, ; 45
	i32 172, ; 46
	i32 213, ; 47
	i32 147, ; 48
	i32 317, ; 49
	i32 286, ; 50
	i32 225, ; 51
	i32 261, ; 52
	i32 251, ; 53
	i32 42, ; 54
	i32 91, ; 55
	i32 83, ; 56
	i32 68, ; 57
	i32 64, ; 58
	i32 88, ; 59
	i32 224, ; 60
	i32 108, ; 61
	i32 307, ; 62
	i32 272, ; 63
	i32 104, ; 64
	i32 37, ; 65
	i32 221, ; 66
	i32 329, ; 67
	i32 274, ; 68
	i32 203, ; 69
	i32 329, ; 70
	i32 121, ; 71
	i32 259, ; 72
	i32 303, ; 73
	i32 321, ; 74
	i32 144, ; 75
	i32 143, ; 76
	i32 55, ; 77
	i32 37, ; 78
	i32 143, ; 79
	i32 218, ; 80
	i32 228, ; 81
	i32 197, ; 82
	i32 299, ; 83
	i32 243, ; 84
	i32 10, ; 85
	i32 16, ; 86
	i32 325, ; 87
	i32 271, ; 88
	i32 53, ; 89
	i32 254, ; 90
	i32 138, ; 91
	i32 103, ; 92
	i32 236, ; 93
	i32 281, ; 94
	i32 118, ; 95
	i32 219, ; 96
	i32 165, ; 97
	i32 328, ; 98
	i32 168, ; 99
	i32 69, ; 100
	i32 187, ; 101
	i32 303, ; 102
	i32 82, ; 103
	i32 0, ; 104
	i32 103, ; 105
	i32 276, ; 106
	i32 119, ; 107
	i32 308, ; 108
	i32 288, ; 109
	i32 80, ; 110
	i32 287, ; 111
	i32 2, ; 112
	i32 116, ; 113
	i32 123, ; 114
	i32 50, ; 115
	i32 130, ; 116
	i32 252, ; 117
	i32 222, ; 118
	i32 84, ; 119
	i32 112, ; 120
	i32 77, ; 121
	i32 300, ; 122
	i32 191, ; 123
	i32 293, ; 124
	i32 205, ; 125
	i32 55, ; 126
	i32 278, ; 127
	i32 181, ; 128
	i32 71, ; 129
	i32 1, ; 130
	i32 277, ; 131
	i32 180, ; 132
	i32 85, ; 133
	i32 174, ; 134
	i32 323, ; 135
	i32 118, ; 136
	i32 182, ; 137
	i32 158, ; 138
	i32 181, ; 139
	i32 216, ; 140
	i32 169, ; 141
	i32 270, ; 142
	i32 244, ; 143
	i32 2, ; 144
	i32 195, ; 145
	i32 34, ; 146
	i32 192, ; 147
	i32 203, ; 148
	i32 124, ; 149
	i32 74, ; 150
	i32 64, ; 151
	i32 163, ; 152
	i32 115, ; 153
	i32 90, ; 154
	i32 201, ; 155
	i32 334, ; 156
	i32 107, ; 157
	i32 20, ; 158
	i32 148, ; 159
	i32 120, ; 160
	i32 60, ; 161
	i32 238, ; 162
	i32 19, ; 163
	i32 214, ; 164
	i32 54, ; 165
	i32 295, ; 166
	i32 94, ; 167
	i32 210, ; 168
	i32 331, ; 169
	i32 57, ; 170
	i32 131, ; 171
	i32 154, ; 172
	i32 43, ; 173
	i32 94, ; 174
	i32 282, ; 175
	i32 194, ; 176
	i32 52, ; 177
	i32 301, ; 178
	i32 164, ; 179
	i32 15, ; 180
	i32 256, ; 181
	i32 219, ; 182
	i32 277, ; 183
	i32 38, ; 184
	i32 69, ; 185
	i32 111, ; 186
	i32 220, ; 187
	i32 101, ; 188
	i32 101, ; 189
	i32 13, ; 190
	i32 13, ; 191
	i32 263, ; 192
	i32 27, ; 193
	i32 130, ; 194
	i32 78, ; 195
	i32 255, ; 196
	i32 111, ; 197
	i32 281, ; 198
	i32 279, ; 199
	i32 214, ; 200
	i32 108, ; 201
	i32 4, ; 202
	i32 28, ; 203
	i32 234, ; 204
	i32 159, ; 205
	i32 327, ; 206
	i32 23, ; 207
	i32 330, ; 208
	i32 51, ; 209
	i32 207, ; 210
	i32 45, ; 211
	i32 128, ; 212
	i32 223, ; 213
	i32 61, ; 214
	i32 208, ; 215
	i32 121, ; 216
	i32 284, ; 217
	i32 247, ; 218
	i32 233, ; 219
	i32 5, ; 220
	i32 253, ; 221
	i32 273, ; 222
	i32 40, ; 223
	i32 126, ; 224
	i32 189, ; 225
	i32 324, ; 226
	i32 273, ; 227
	i32 210, ; 228
	i32 324, ; 229
	i32 139, ; 230
	i32 151, ; 231
	i32 87, ; 232
	i32 92, ; 233
	i32 257, ; 234
	i32 335, ; 235
	i32 254, ; 236
	i32 312, ; 237
	i32 228, ; 238
	i32 240, ; 239
	i32 285, ; 240
	i32 198, ; 241
	i32 290, ; 242
	i32 255, ; 243
	i32 135, ; 244
	i32 98, ; 245
	i32 5, ; 246
	i32 320, ; 247
	i32 107, ; 248
	i32 323, ; 249
	i32 35, ; 250
	i32 156, ; 251
	i32 160, ; 252
	i32 157, ; 253
	i32 84, ; 254
	i32 249, ; 255
	i32 145, ; 256
	i32 89, ; 257
	i32 21, ; 258
	i32 250, ; 259
	i32 53, ; 260
	i32 218, ; 261
	i32 327, ; 262
	i32 63, ; 263
	i32 56, ; 264
	i32 6, ; 265
	i32 99, ; 266
	i32 217, ; 267
	i32 19, ; 268
	i32 157, ; 269
	i32 86, ; 270
	i32 31, ; 271
	i32 47, ; 272
	i32 66, ; 273
	i32 68, ; 274
	i32 318, ; 275
	i32 174, ; 276
	i32 258, ; 277
	i32 3, ; 278
	i32 297, ; 279
	i32 49, ; 280
	i32 26, ; 281
	i32 225, ; 282
	i32 190, ; 283
	i32 179, ; 284
	i32 167, ; 285
	i32 110, ; 286
	i32 14, ; 287
	i32 252, ; 288
	i32 65, ; 289
	i32 29, ; 290
	i32 25, ; 291
	i32 95, ; 292
	i32 170, ; 293
	i32 14, ; 294
	i32 298, ; 295
	i32 206, ; 296
	i32 31, ; 297
	i32 0, ; 298
	i32 105, ; 299
	i32 16, ; 300
	i32 128, ; 301
	i32 235, ; 302
	i32 267, ; 303
	i32 93, ; 304
	i32 256, ; 305
	i32 11, ; 306
	i32 88, ; 307
	i32 246, ; 308
	i32 175, ; 309
	i32 279, ; 310
	i32 322, ; 311
	i32 73, ; 312
	i32 170, ; 313
	i32 3, ; 314
	i32 266, ; 315
	i32 7, ; 316
	i32 322, ; 317
	i32 46, ; 318
	i32 29, ; 319
	i32 189, ; 320
	i32 160, ; 321
	i32 269, ; 322
	i32 114, ; 323
	i32 299, ; 324
	i32 332, ; 325
	i32 180, ; 326
	i32 123, ; 327
	i32 284, ; 328
	i32 224, ; 329
	i32 208, ; 330
	i32 161, ; 331
	i32 133, ; 332
	i32 289, ; 333
	i32 59, ; 334
	i32 185, ; 335
	i32 140, ; 336
	i32 85, ; 337
	i32 32, ; 338
	i32 236, ; 339
	i32 12, ; 340
	i32 286, ; 341
	i32 173, ; 342
	i32 233, ; 343
	i32 152, ; 344
	i32 96, ; 345
	i32 292, ; 346
	i32 246, ; 347
	i32 62, ; 348
	i32 204, ; 349
	i32 159, ; 350
	i32 307, ; 351
	i32 197, ; 352
	i32 66, ; 353
	i32 90, ; 354
	i32 81, ; 355
	i32 49, ; 356
	i32 202, ; 357
	i32 145, ; 358
	i32 304, ; 359
	i32 183, ; 360
	i32 240, ; 361
	i32 76, ; 362
	i32 93, ; 363
	i32 296, ; 364
	i32 137, ; 365
	i32 92, ; 366
	i32 278, ; 367
	i32 300, ; 368
	i32 178, ; 369
	i32 237, ; 370
	i32 302, ; 371
	i32 114, ; 372
	i32 44, ; 373
	i32 161, ; 374
	i32 6, ; 375
	i32 105, ; 376
	i32 72, ; 377
	i32 199, ; 378
	i32 62, ; 379
	i32 41, ; 380
	i32 226, ; 381
	i32 175, ; 382
	i32 155, ; 383
	i32 58, ; 384
	i32 36, ; 385
	i32 196, ; 386
	i32 204, ; 387
	i32 223, ; 388
	i32 23, ; 389
	i32 165, ; 390
	i32 290, ; 391
	i32 313, ; 392
	i32 288, ; 393
	i32 283, ; 394
	i32 142, ; 395
	i32 316, ; 396
	i32 198, ; 397
	i32 91, ; 398
	i32 231, ; 399
	i32 149, ; 400
	i32 239, ; 401
	i32 164, ; 402
	i32 268, ; 403
	i32 8, ; 404
	i32 171, ; 405
	i32 33, ; 406
	i32 109, ; 407
	i32 249, ; 408
	i32 314, ; 409
	i32 283, ; 410
	i32 195, ; 411
	i32 221, ; 412
	i32 276, ; 413
	i32 169, ; 414
	i32 250, ; 415
	i32 142, ; 416
	i32 310, ; 417
	i32 61, ; 418
	i32 209, ; 419
	i32 146, ; 420
	i32 209, ; 421
	i32 83, ; 422
	i32 76, ; 423
	i32 192, ; 424
	i32 193, ; 425
	i32 132, ; 426
	i32 27, ; 427
	i32 9, ; 428
	i32 95, ; 429
	i32 280, ; 430
	i32 139, ; 431
	i32 215, ; 432
	i32 115, ; 433
	i32 11, ; 434
	i32 213, ; 435
	i32 106, ; 436
	i32 176, ; 437
	i32 21, ; 438
	i32 248, ; 439
	i32 262, ; 440
	i32 335, ; 441
	i32 242, ; 442
	i32 35, ; 443
	i32 229, ; 444
	i32 48, ; 445
	i32 315, ; 446
	i32 32, ; 447
	i32 230, ; 448
	i32 59, ; 449
	i32 136, ; 450
	i32 116, ; 451
	i32 285, ; 452
	i32 328, ; 453
	i32 57, ; 454
	i32 200, ; 455
	i32 8, ; 456
	i32 79, ; 457
	i32 241, ; 458
	i32 113, ; 459
	i32 245, ; 460
	i32 104, ; 461
	i32 302, ; 462
	i32 316, ; 463
	i32 172, ; 464
	i32 117, ; 465
	i32 310, ; 466
	i32 280, ; 467
	i32 235, ; 468
	i32 78, ; 469
	i32 1, ; 470
	i32 291, ; 471
	i32 87, ; 472
	i32 297, ; 473
	i32 330, ; 474
	i32 227, ; 475
	i32 331, ; 476
	i32 314, ; 477
	i32 191, ; 478
	i32 270, ; 479
	i32 162, ; 480
	i32 4, ; 481
	i32 241, ; 482
	i32 26, ; 483
	i32 220, ; 484
	i32 34, ; 485
	i32 119, ; 486
	i32 39, ; 487
	i32 18, ; 488
	i32 309, ; 489
	i32 54, ; 490
	i32 312, ; 491
	i32 22, ; 492
	i32 125, ; 493
	i32 156, ; 494
	i32 248, ; 495
	i32 133, ; 496
	i32 304, ; 497
	i32 178, ; 498
	i32 229, ; 499
	i32 150, ; 500
	i32 216, ; 501
	i32 122, ; 502
	i32 30, ; 503
	i32 134, ; 504
	i32 102, ; 505
	i32 136, ; 506
	i32 268, ; 507
	i32 155, ; 508
	i32 99, ; 509
	i32 127, ; 510
	i32 217, ; 511
	i32 71, ; 512
	i32 74, ; 513
	i32 325, ; 514
	i32 253, ; 515
	i32 271, ; 516
	i32 306, ; 517
	i32 138, ; 518
	i32 176, ; 519
	i32 126, ; 520
	i32 73, ; 521
	i32 113, ; 522
	i32 263, ; 523
	i32 187, ; 524
	i32 294, ; 525
	i32 154, ; 526
	i32 317, ; 527
	i32 333, ; 528
	i32 291, ; 529
	i32 120, ; 530
	i32 239, ; 531
	i32 177, ; 532
	i32 334, ; 533
	i32 301, ; 534
	i32 129, ; 535
	i32 135, ; 536
	i32 188, ; 537
	i32 79, ; 538
	i32 48, ; 539
	i32 242, ; 540
	i32 186, ; 541
	i32 75, ; 542
	i32 65, ; 543
	i32 190, ; 544
	i32 100, ; 545
	i32 86, ; 546
	i32 318, ; 547
	i32 45, ; 548
	i32 63, ; 549
	i32 269, ; 550
	i32 182, ; 551
	i32 39, ; 552
	i32 42, ; 553
	i32 232, ; 554
	i32 298, ; 555
	i32 162, ; 556
	i32 100, ; 557
	i32 237, ; 558
	i32 188, ; 559
	i32 137, ; 560
	i32 22, ; 561
	i32 67, ; 562
	i32 305, ; 563
	i32 127, ; 564
	i32 77, ; 565
	i32 261, ; 566
	i32 166, ; 567
	i32 158, ; 568
	i32 305, ; 569
	i32 7, ; 570
	i32 313, ; 571
	i32 51, ; 572
	i32 275, ; 573
	i32 306, ; 574
	i32 146, ; 575
	i32 185, ; 576
	i32 141, ; 577
	i32 102, ; 578
	i32 201, ; 579
	i32 295, ; 580
	i32 125, ; 581
	i32 122, ; 582
	i32 144, ; 583
	i32 41, ; 584
	i32 70, ; 585
	i32 43, ; 586
	i32 166, ; 587
	i32 75, ; 588
	i32 319, ; 589
	i32 167, ; 590
	i32 196, ; 591
	i32 129, ; 592
	i32 247, ; 593
	i32 70, ; 594
	i32 171, ; 595
	i32 194, ; 596
	i32 260, ; 597
	i32 234, ; 598
	i32 206, ; 599
	i32 267, ; 600
	i32 153, ; 601
	i32 47, ; 602
	i32 110, ; 603
	i32 211, ; 604
	i32 50, ; 605
	i32 98, ; 606
	i32 33, ; 607
	i32 25, ; 608
	i32 168, ; 609
	i32 24, ; 610
	i32 140, ; 611
	i32 80, ; 612
	i32 326, ; 613
	i32 56, ; 614
	i32 260, ; 615
	i32 265, ; 616
	i32 12, ; 617
	i32 222, ; 618
	i32 264, ; 619
	i32 251, ; 620
	i32 184, ; 621
	i32 18, ; 622
	i32 186, ; 623
	i32 333, ; 624
	i32 141, ; 625
	i32 15, ; 626
	i32 17, ; 627
	i32 124, ; 628
	i32 193, ; 629
	i32 89, ; 630
	i32 151, ; 631
	i32 24, ; 632
	i32 36, ; 633
	i32 81, ; 634
	i32 311, ; 635
	i32 289, ; 636
	i32 326, ; 637
	i32 149, ; 638
	i32 82, ; 639
	i32 309, ; 640
	i32 177, ; 641
	i32 215, ; 642
	i32 321, ; 643
	i32 44, ; 644
	i32 28, ; 645
	i32 332, ; 646
	i32 262, ; 647
	i32 259, ; 648
	i32 109, ; 649
	i32 112, ; 650
	i32 9, ; 651
	i32 296, ; 652
	i32 46, ; 653
	i32 293, ; 654
	i32 163, ; 655
	i32 179, ; 656
	i32 150, ; 657
	i32 320, ; 658
	i32 227, ; 659
	i32 264, ; 660
	i32 40, ; 661
	i32 17, ; 662
	i32 148, ; 663
	i32 10, ; 664
	i32 245, ; 665
	i32 266, ; 666
	i32 132, ; 667
	i32 315, ; 668
	i32 212, ; 669
	i32 282, ; 670
	i32 96 ; 671
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

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
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
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
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" }

; Metadata
!llvm.module.flags = !{!0, !1, !7, !8, !9, !10}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 0d97e20b84d8e87c3502469ee395805907905fe3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"branch-target-enforcement", i32 0}
!8 = !{i32 1, !"sign-return-address", i32 0}
!9 = !{i32 1, !"sign-return-address-all", i32 0}
!10 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
