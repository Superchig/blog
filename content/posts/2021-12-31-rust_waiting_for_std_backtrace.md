<!--

---
title: "Rust: waiting for std::backtrace to be stabilized"
date: 2021-12-31T20:45:20-08:00
draft: false
tags: ["Rust", "programming"]
---

-->

Every so often, I've worked on a terminal file manager called
[`rolf`](https://github.com/Superchig/rolf). Modeled after the excellent
[lf](https://github.com/gokcehan/lf) file manager, this (woefully incomplete)
project exists primarily to help me learn the Rust programming language.

When it comes to error handling, I'd love to use the [anyhow
library](https://github.com/dtolnay/anyhow) to help me concisely handle
multiple different error types. However, support for backtraces is dependent
on the `std::backtrace` module, which is currently only available in [Rust
nightly](https://github.com/rust-lang/rust/issues/53487). Since I only
occasionally work on `rolf`, building it with later versions of Rust (with
minimal-to-no changes) is a goal of mine. As a result, Rust nightly is off the
table for my purposes.

When errors occur, I find backtraces invaluable. Since `std::backtrace` is
locked behind nightly, it seems like using anyhow for error propagation will
actually make it more difficult for me to access them. Instead, I've been
making liberal use of `unwrap()` (even when I'm aware that a function call may
legitimately fail), which gives me stack traces. Of course, this comes at the
cost of crashing the program on these errors.

Is this bad programming practice? Quite possibly. Certainly, no one should use
`rolf` for anything vital. To develop the project to a certain level of
maturity, I would need to inspect my uses of `unwrap()` by hand and properly
handle legitimate errors. However, I may never get to that, and so crashing on
errors is acceptable to me.
