# Unity3d高级编程主程手记 整理


## C#技术要点
### 1.Unity3D中C#的底层原理（Mono 和 IL2CPP）
#### 那么IL2CPP的编译和运行过程是怎么样的呢？
#### 首先还是由Mono将C#语言翻译成IL，IL2CPP在得到中间语言IL后，将它们重新变回C++代码，再由各个平台的C++编译器直接编译成能执行的机器码。
### 2.List
#### IList源码网址为：https://referencesource.microsoft.com/#mscorlib/system/collections/ilist.cs,5d74f6adfeaf6c7d。
#### IReadOnlyList源码网址为：https://referencesource.microsoft.com/#mscorlib/system/collections/generic/ireadonlylist.cs,b040fb780bdd59f4。