# Material-Donation-Supervision
软工

## 客户端

* WPF(.net Core3.1)
* UI设计语言为谷歌的MaterialDesign，使用[MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)和[MaterialDesignExtensions](https://github.com/spiegelp/MaterialDesignExtensions)来实现
    * 注意MaterialDesignExtensions的TabStepper疑似存在一些问题，在Tab中使用某些MD控件会导致渲染的时候有很多莫名其妙的格线，应该是它的BUG，请小心使用

### 关于风格统一

尽量使用MD的控件和Style

## 网络模块

1. 不确定效率怎么样，特别是服务器方面
2. 错误如何体现，例如网络断了
3. 客户端目前还需要封装一层，使用线程调用网络模块，否则占用UI线程可能会造成卡顿