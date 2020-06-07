# Material-Donation-Supervision

软工文档

测试的时候，注意，VS2019可以同时开多个程序的。在解决方案的属性中使用多个startup，同时启动服务器和数据库

![](images/1.png)
![](images/2.png)

## 客户端

* 使用WPF框架(基于.net Core3.1，注意VS2017并不支持开发.NetCore3，请先下载VS2019)
* UI设计语言为谷歌的MaterialDesign，使用[MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)和[MaterialDesignExtensions](https://github.com/spiegelp/MaterialDesignExtensions)这两个工具包来实现
    * 注意MaterialDesignExtensions的TabStepper疑似存在一些问题，在Tab中使用某些MD控件会导致渲染的时候有很多莫名其妙的格线，应该是它的BUG，请小心使用
* 客户端的报错尽量不要使用```MessageBox.Show```，UI很难看，MainWindow有一个SnackBar，用那个。
* 最终提交前请在Application那里catch所有未被catch的异常，防止客户端遇到异常直接崩溃。并且在MainPage中开启根据用户类型显示Tab。测试阶段可以关闭这些代码方便测试。

### 关于风格统一

尽量使用MD的控件和Style

## 网络模块

1. 异常处理问题，404，连不上服务器时客户端需要TryCatch来防止程序崩溃（可能可以统一处理？我没研究过）
2. 边际问题，网络请求过程中可能需要Disable部分UI控件，防止意料之外的用户操作。目前已支持```Task.DisableElements(...)```操作，在Task执行过程中Disable控件，结束后自动Enable这些控件，具体使用方法参考```MDS.Client.LoginDialog.Login()```
3. 加载动画。网络请求时可以有加载动画的。目前已支持```Task.Progress(bar)```，参数是一个```ProgressBar```（定义在```MainWindow```中），具体使用方法参考```MDS.Client.MainWindow.Window_Loaded()```。NavidationPages里面每个Page都可以通过```ParentWindow```访问```MainWindow```的成员。
4. 请使用GetAsync而不是Get，异步方法可以防止网络比较慢时UI线程卡死。异步方法需要```await```，带```await```的方法需要是```async```，具体参考微软的文档。

## 服务器

* 使用SQLServer 2017
* 如果想要获得刚刚插入的代码所在行的某一个值，可以使用下面的代码，参考了[StackOveflow](https://stackoverflow.com/questions/18373461/execute-insert-command-and-return-inserted-id-in-sql)。这段代码可以用于插入表，同时想要获得插入的记录的自增属性。
```C#
com.CommandText = $"INSERT INTO Tranc (UserId, Address) OUTPUT INSERTED.TransactionId values ({UserId}, '{request.Address}')";
int modified = Convert.ToInt32(com.ExecuteScalar());
```
* 多条修改请使用Transaction

## DTO

客户端通过DTO与服务器进行数据沟通，客户端会填写DTO中的request并发送给服务器，服务器根据收到的的request返回response。不同的操作request和response的结构不同，详见DTO

### 一个物资的数据包的全部信息如下：    
    string GUID          订单号  
    string Name          物资名称  
    int Quantity         物资数量  
    int StartID          发货人ID  
    int FinishID         收货人ID  
       （对于捐赠，发货人为用户、收货人为管理员；对于分发，发货人为管理员、收货人为受捐赠者）  
    string Departure     出发地  
    string Destination   目的地  
    枚举类型 State        当前状态  
    DateTime StartTime   订单开始时间  
    DateTime FinishTime  订单完成时间 

### 配送员DTO：（定义在DTO下DeliveryData.cs中）

      状态State有三种，定义如下：  
         public enum DeliveryState  
         {  
            Waiting,    // 待接单（配送员尚未取得物资）  
            Processing, // 配送中（物资在配送员手中）  
            Finished    // 已完成  
         }
      可能的操作如下：  
      1.发送填充自己的ID和状态的DeliveryListRequest，获取与所发送状态相同的、包含物资全部信息的Item类的表List<Item>  
      2.发送填充自己的ID和状态的DeliveryListNumRequest，获取与所发送状态相同的物资总数  
      3.发送填充自己的ID、订单号和验证ID的DeliveryMoveRequest，返回代表操作结果的整数  
         此操作为向服务器申请将此订单的状态转移至下一状态，即  
            a.Waiting转移为Processing（验证ID为发货人ID）  
            b.Processing转移为Finished（验证ID为收货人ID）  
         0表示成功  
         1表示验证ID错误  
         2表示订单状态非Waiting或Processing  
         3表示其它错误  
