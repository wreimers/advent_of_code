mod day_01;

mod prelude {
    pub use crate::day_01::{MyStruct,Another};
}

use crate::prelude::*;

// struct SumArgs {
//     n1: i32,
//     n2: i32,
// }

struct SumArgs(i32, i32);

fn add_numbers(args: &SumArgs) -> i32 {
    // args.n1 + args.n2
    args.0 + args.1
}

fn main() {
    let _ms = MyStruct {};
    let _as = Another {};

    // let args = SumArgs {n1: 10, n2: 20};
    let args = SumArgs(10, 20);
    let sum = add_numbers(&args);

    // println!("{} + {} = {}", args.n1, args.n2, sum);
    println!("{} + {} = {}", args.0, args.1, sum);
}
