mod day_01;

mod prelude {
    pub use crate::day_01::{MyStruct,Another};
}

use crate::prelude::*;

struct SumArgs {
    n1: i32,
    n2: i32,
}

impl SumArgs {
    fn new(n1: i32, n2: i32) -> Self {
        Self { n1, n2 }
    }

    fn add_numbers(&self) -> i32 {
        self.n1 + self.n2
    }
}

fn main() {
    let _ms = MyStruct {};
    let _as = Another {};

    let args = SumArgs::new(10, 20);
    let sum = args.add_numbers();

    println!("{} + {} = {}", args.n1, args.n2, sum);
}
