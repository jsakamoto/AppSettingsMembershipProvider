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

![figure.1](https://raw.github.com/jsakamoto/AppSettingsMembershipProvider/master/content/fig01.PNG)

"Windows Azure Websites" または "AppHarbor" は、その PaaS サービスのダッシュボード上から、appSettings セクションの値を構成できます。

それゆえ、あなたは **ユーザーアカウントの管理機能をあなたのWebアプリケーションの上に実装する必要がなくなります。**   PaaS サービスのダッシュボードがあなたのアプリケーションのユーザーアカウント管理ページになります。

## How to install? / インストール方法

You can install this provider as a NuGet package into your ASP.NET Web Appliction project on Visual Studio via NuGet.org.

Visual Studio 上の ASP.NET Web アプリケーションに NuGet パッケージとして NuGet.org 経由でインストールできます。

```
PM>Install-Package AppSettingsMembershipProvider
```

## How to use? / 使い方

After installation, you should configure appSettings.

### Syntax of appSettings entry / 構文

key="User. *{username}* " value=" *[{hash algorithm, "sha1", "md5"}* : *[{salt}* : *]] {hash of password}* "

key="User. *{ユーザー名}* " value=" *[{ハッシュアルゴリズム, "sha1", "md5"}* : *[{ソルト}* : *]] {パスワードのハッシュ値}* "
    
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

## But I need to build login page, do not me? / しかしログインページは作る必要がありますね?

You have some choice, one of the alternate way is using HTTP Basic Authentication by ["HttpAuthenticationModule"](https://httpauthmod.codeplex.com/).

"HttpAuthenticationModule" による HTTP 基本認証を使うことができます。

So you can represent Basic Athentication dialog for login witch provided by web browser instead of login page whice you have to build. 

ログインページを作る代わりに、Webブラウザの基本認証ダイアログを表示させることができます。

At first, you should install "HttpAuthenticationModule" in NuGet package console. 

まずはじめに、NuGetパッケージコンソールから "HttpAuthenticationModule" をインストールします。

    PM>Install-Package HttpAuthenticationModule

Next, you should configure the httpAuth section at web.config to use membership provider to validate user.

次にユーザーの検証のためにメンバシッププロバイダを使うよう web.config の httpAuth セクションを構成します。

    <httpAuth mode="Basic" realm="Secret">
      <credentials source="MembershipProvider"/>
    </httpAuth>

At last, you should configure authorization section to reject anonymous user access.

最後に、匿名ユーザーからのアクセスを拒否するよう、web.config の authorization セクションを構成します。

    <system.web>
      ...
      <authorization>
        <deny users="?" />
      </authorization>
    </system.web>

Then user can authenticate by HTTP Basci Autentication dialog.

これでユーザーは HTTP 基本認証ダイアログによって認証可能となります。

![figure.3](https://raw.github.com/jsakamoto/AppSettingsMembershipProvider/master/content/fig03.PNG)

## Basic auth? Is it safety? / 基本認証? それは安全ですか?

Of course, HTTP Basic Authentication trafic contains plain text password.

もちろん、HTTP 基本認証は通信に平文のパスワードを含みます。

But you can chose HHTPS protocol on Windows Azure Websites or AppHarbor. If you use HTTPS protocol then comunication between a browser and a server is encrypted, so this is enugh secure.

しかしあなたは、Windows Azure Websites または AppHarbor で HTTPS プロトコルを使う選択肢があります。もしブラウザとサーバー間の通信に HTTPS プロトコルを使えば、暗号化されて対話するのでじゅうぶん安全です。

You don't have to anything to HTTPS access, You only change the url of the app to access from "http:..." to "https:...".

HTTPS でアクセスするのに何かをする必要はありません、単にそのアプリへのアクセスの URL を "http:..." から "https:..." に変えるだけです。

If you want to restrict HTTPS access only, you have a choice to install the ["AppOfflineModule"](https://github.com/jsakamoto/appofflinemodule) via NuGet.

もし HTTPS でのアクセスのみに制限したい場合は、"AppOfflineModule" を使う選択肢があります。

At first you should install AppOfflineModule in NuGet package console.

まずはじめに、NuGetパッケージコンソールから "AppOfflineModule" をインストールします。

    PM>Install-Package AppOfflineModule

Next, you should write some code in Global.asax like a follows:

次に、Global.asax に下記のような少量のコードを書きます。

    protected void Application_Start()
    {
        ...
        AppOfflineModule.Filter.IsEnable = () =>
            HttpContext.Current.Request.IsSecureConnection == false;
    }

Then, all access via HTTP, not HTTPS, is reject ( got HTTP 404 Not Found ).

すると、HTTPS ではなく HTTP 経由のアクセスはすべて遮断されます （HTTP 404 Not Found が返ります）。
