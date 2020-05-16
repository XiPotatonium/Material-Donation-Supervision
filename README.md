# Material-Donation-Supervision
软工
   
## 客户端

* WPF(.net Core3.1)
* UI设计语言为谷歌的MaterialDesign，使用[MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)和[MaterialDesignExtensions](https://github.com/spiegelp/MaterialDesignExtensions)来实现
    * 注意MaterialDesignExtensions的TabStepper疑似存在一些问题，在Tab中使用某些MD控件会导致渲染的时候有很多莫名其妙的格线，应该是它的BUG，请小心使用
### 数据包信息
#### 一个物资的数据包的全部信息如下：  
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
    
### DTO
客户端通过DTO与服务器进行数据沟通，客户端会填写DTO中的request并发送给服务器，服务器根据收到的的request返回response。不同的操作request和response的结构不同，详见DTO
#### 配送员DTO：  
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

### 关于风格统一

尽量使用MD的控件和Style

## 网络模块

1. 不确定效率怎么样，特别是服务器方面
2. 错误如何体现，例如网络断了
3. 客户端目前还需要封装一层，使用线程调用网络模块，否则占用UI线程可能会造成卡顿

