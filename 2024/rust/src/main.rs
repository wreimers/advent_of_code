mod day_01;

mod prelude {
    pub use crate::day_01::{MyStruct,Another};
}

use crate::prelude::*;


fn main() {
    println!("Hello, world!");
    let _ms = MyStruct {};
    let _as = Another {};
}
