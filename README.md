AppSettingsMembershipProvider
=============================

## What's this? / これは何か?

This is the Membership Provider for ASP.NET.

This provider allows you to store username and password of membership users into **appSettings section** web.config.

これは ASP.NET のメンバシッププロバイダーです。

このプロバイダーは、メンバシップユーザーのユーザー名とパスワードを、 **web.config の appSettings 構成に格納** することができるようになります。

## Why appSettings section? / なぜ appSettings 構成なのか?

"Windows Azure Websites" or "AppHarbor" can configure variable of appSettings section from dashboard of those PaaS service.

Therefore, **you have no need to build user account administration feature on your web app.**
The dashborad of PaaS service became user account administration page for your app.

"Windows Azure Websites" または "AppHarbor" は、その PaaS サービスのダッシュボード上から、appSettings セクションの値を構成できます。

それゆえ、あなたは **ユーザーアカウントの管理機能をあなたのWebアプリケーションの上に実装する必要がなくなります。**
PaaS サービスのダッシュボードがあなたのアプリケーションのユーザーアカウント管理ページになります。

## How to install? / インストール方法

You can install this provider as a NuGet package into your ASP.NET Web Appliction project on Visual Studio via NuGet.org.

Visual Studio 上の ASP.NET Web アプリケーションに NuGet パッケージとして NuGet.org 経由でインストールできます。

```
PM>Install-Package AppSettingsMembershipProvider
```

## How to use? / 使い方

After installation, you should configure appSettings.

### Syntax of appSettings entry / 構文

key="User.*{username}*" value="*[{hash algorithm, "sha1", "md5"}*:*[{salt}*:*]]{hash of password}*"

key="User.*{ユーザー名}*" value="*[{ハッシュアルゴリズム, "sha1", "md5"}*:*[{ソルト}*:*]]{パスワードのハッシュ値}*"
    
### Exapmle: user name and password is "foo" and "bar" / 例: ユーザー名とパスワードが "foo" と "bar"
    
#### Case 1) 

If hash algorithm missing, use "sha1". "62cdb7..." is sha1 hash of "bar".

ハッシュアルゴリズムが省略された場合は "sha1" が使われます。"62cdb7..." は "bar" の sha1 ハッシュ値です。

    <add key="User.foo" value="62cdb7020ff920e5aa642c3d4066950dd1f01f4d" />
    
#### Case 2)

Using "md5", but no salt. "37b51d..." is md5 hash of "bar".

md5 ハッシュアルゴリズムを指定、ただしソルトは省略した例。"37b51d..." は "bar" の md5 ハッシュ値です。

    <add key="User.foo" value="md5:37b51d194a7513e45b56f6524f2d51f2" />
    
#### Case 3)

 "2058a..." is md5 hash of "boo:bar".

 "2058a..." は "boo:bar" の md5 ハッシュ値です。

    <add key="User.foo" value="md5:boo:2058a0df9e5b5cb970a1c8d7783a8ec8" />

## Trade-off / 代償

This provider can **only** validate username and password, and get membership user by name.

This provider **does not** provide follows functions.

- Create, Delete, Update, Approve, Lock out user.
- Change password, and password hint.
- etc, etc...

このプロバイダはユーザー名とパスワードの検証、およびユーザー名によるメンバシップユーザーの取得ができるのみです。

以下の機能は提供されません。

- ユーザーの作成、削除、更新、承認、ロックアウト
- パスワードの変更、パスワードヒント
- 等々
