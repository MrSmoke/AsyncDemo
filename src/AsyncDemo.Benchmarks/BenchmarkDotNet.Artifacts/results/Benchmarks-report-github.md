``` ini

BenchmarkDotNet=v0.10.8, OS=Windows 10 Redstone 1 (10.0.14393)
Processor=Intel Core i7-3770K CPU 3.50GHz (Ivy Bridge), ProcessorCount=8
Frequency=3417994 Hz, Resolution=292.5693 ns, Timer=TSC
dotnet cli version=1.0.4
  [Host]     : .NET Core 4.6.25211.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25211.01, 64bit RyuJIT


```
 |        Method |     Mean |     Error |    StdDev |
 |-------------- |---------:|----------:|----------:|
 |    MethodHell | 15.62 ms | 0.0107 ms | 0.0100 ms |
 | NotMethodHell | 15.62 ms | 0.0076 ms | 0.0067 ms |
