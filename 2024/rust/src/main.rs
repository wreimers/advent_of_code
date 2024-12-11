mod day_01;

mod prelude {
    pub use crate::day_01::{MyStruct,Another};
}

use crate::prelude::*;

fn my_print() {
    println!("Hello, world!");
}

fn add_numbers(n1: i32, n2: i32) -> i32 {
    n1 + n2
}

fn main() {
    let _ms = MyStruct {};
    let _as = Another {};
    let n1 = 10;
    let n2 = 20;
    // my_print();
    let sum = add_numbers(n1, n2);
    println!("{} + {} = {}", n1, n2, sum);
}
