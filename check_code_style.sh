#!/bin/sh

grep -n '................................................................................' $(find . -name \*.cs)

# TODO: check for tabs rather than spaces
# TODO: check for trailing whitespace on lines
# TODO: check for single newline at end of file
# TODO: count TODO's
