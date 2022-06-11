module Platform

#if FABLE_COMPILER_RUST

open Fable.Core

module Performance =
    [<Erase; Emit("std::time::Duration")>]
    type Duration =
        abstract as_millis: unit -> uint64 // actually u128
        abstract as_secs_f64: unit -> float

    [<Erase; Emit("std::time::Instant")>]
    type Instant =
        abstract duration_since: Instant -> Duration
        abstract elapsed: unit -> Duration

    [<Emit("std::time::Instant::now()")>]
    let now(): Instant = nativeOnly

let measureTime (f: unit -> 'T): 'T * float =
    let t0 = Performance.now()
    let res = f ()
    let t1 = Performance.now()
    let duration = t1.duration_since(t0)
    let elapsed = duration.as_secs_f64()
    res, elapsed * 1000.0

#endif

#if !FABLE_COMPILER // .NET

let measureTime (f: unit -> 'T): 'T * float =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let res = f ()
    sw.Stop()
    res, sw.Elapsed.TotalMilliseconds

#endif

