# GEFramework框架：
## 介绍：
GEFramework是GameFramework和ET6.0框架结合的产物（毕竟两个大佬的框架让我难于取舍，于是就全要了）。
## 本框架核心思想：
将GameFramework的核心GF代码部分接入ET6.0的mono层，原UGF部分按照ET6.0的ECS模式进行重构接入ModelView和HotfixView（以后可能会更新更多的内容，不只是UGF）。
## 本框架优势：
1..完整保留了GameFramework的OOP和ET6.0的ECS思想。
<br>2.可以热更新重构后的UGF框架。
<br>（因为底层的GF没有热更的必要，如果底层GF出问题，那就不是热更能够解决的东西了。）
<br>3.业务层依旧可以按照ET6.0的ECS思想，完整使用底层GameFramework用OOP写出的功能。
<br>（UGF已经作为两个框架的桥梁进行ECS重构了。）
## GameFramework和ET6.0的优势：
[烟雨大佬关于两者优势的总结](https://www.zhihu.com/question/268285328/answer/2180741715)  
### GameFramework
完善的UI框架
完善的基于Unity Gameobject的Entity Component组件式编程
<br> 完善的资源管理模块，包括资源热更新模块，分组更新（边玩边下），VFS（Virtual File System，可用于ab加密以及提高读取性能），AB无感知的资源加载模块
<br> Task，Fsm，DataNode，Network，EventSystem等多个游戏常用功能模块
### ET6.0
能跑机器人压力测试工具，因为ET强制分离了Model和View层（强制到你没有办法在Model层获取Unity GameObject），
<br>所以可以十分方便的剥离Model层代码封装成机器人模块，一键启动大量的机器人实例（每个机器人都是一个控制台程序）对服务端进行压力测试，这极大的节省了人力成本
<br>（比如平时的BUG测试，组队测试，房间测试，匹配测试等，用机器人模块可以十分方便的完成）
<br>特化的ECS架构，开发迭代起来结构清晰，酣畅淋漓ETTask异步支持，可以用同步的方式书写异步代码来避免回调炼狱同时提供了TCP和KCP两种网络通信方式，可以无感知切换
## 目前完成进度_(´ཀ`」 ∠)_：
完成：
<br>UGF的ObjectPool相关的UGF重构。
<br>进行中：
<br>UGF的BaseComponent部分相关的重构。
<br>UGF的UIComponent相关的重构。
<br>未完成：
 <br>。。。。。（总之就是还有非常多。呜呜呜。。。）
 ## 友情链接：
 [GameFramework](https://github.com/EllanJiang/GameFramework)  
 [ET](https://github.com/egametang/ET)  
  ## GameFramework强人锁男畅聊QQ群：806805807
 
