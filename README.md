# Chris Chang's Blog

## Setup

You may have to install Ruby with a specific OpenSSL version. On Arch Linux
(and with the [frum](https://github.com/TaKO8Ki/frum) version manager), you
can use the following commands to install Ruby 2.7.7 with OpenSSL 1.1.

```sh
PKG_CONFIG_PATH=/usr/lib/openssl-1.1/pkgconfig \
CFLAGS+=" -I/usr/include/openssl-1.1" \
LDFLAGS+=" -L/usr/lib/openssl-1.1 -lssl" \
frum install 2.7.7
```

After you have the relevant Ruby version installed, you can use the following
commands to install the required Ruby gems.

```sh
gem install bundler
bundle install
```

## Serve files for development

```sh
bundle exec jekyll serve
```
