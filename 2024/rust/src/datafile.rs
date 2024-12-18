use std::fs::File;
use std::io::{BufRead, BufReader};

pub struct DataFile {
    pub pathname: String,
    pub lines: Vec<Vec<i32>>,
}

impl DataFile {
    pub fn new(pathname: &str) -> Self {
        Self {
            pathname: pathname.to_string(),
            lines: DataFile::read_lines(pathname),
        }
    }

    fn read_lines(pathname: &str) -> Vec<Vec<i32>> {
        let mut result: Vec<Vec<i32>> = Vec::new();
        let f = File::open(pathname).expect("Unable to open file");
        let f = BufReader::new(f);
        for line in f.lines() {
            let line = line.expect("Unable to read line");
            let parsed_line: Vec<i32> = DataFile::parse_line(line);
            result.push(parsed_line);
        }
        result
    }

    fn parse_line(line: String) -> Vec<i32> {
        Vec::new()
    }
}
