# DT Sequence API

* DOTween.Sequence()：创建一个新的Sequence对象。
* Append(Tween)：将Tween对象添加到序列的末尾，按照添加的顺序播放。
* Prepend(Tween)：将Tween对象添加到序列的开头，按照添加的顺序播放。
* Insert(float, Tween)：在指定的时间（以秒为单位）将Tween对象插入到序列中。
* Join(Tween)：将Tween对象与序列中的上一个Tween对象同时播放。
* JoinSequence(Sequence)：将另一个Sequence对象中的所有Tween对象添加到当前序列中，并同时播放。
* AppendInterval(float)：在序列中添加指定的延迟时间（以秒为单位）。
* PrependInterval(float)：在序列的开头添加指定的延迟时间（以秒为单位）。
* InsertInterval(float, float)：在指定的时间（以秒为单位）插入指定的延迟时间。
* AppendCallback(Action)：在序列的末尾添加一个回调函数，当Tween对象播放完成时调用。
* PrependCallback(Action)：在序列的开头添加一个回调函数，当Tween对象播放完成时调用。
* SetDelay(float)：设置整个序列的延迟时间。
* SetAutoKill(bool)：设置当序列播放完毕后是否自动销毁。
* SetLoops(int)：设置序列循环播放的次数。使用-1表示无限循环。
* SetEase(Ease)：设置序列中所有Tween对象的缓动函数。
* SetUpdate(UpdateType)：设置Tween对象更新时使用的UpdateType。默认情况下为UpdateType.Update。
* Pause()：暂停序列的播放。
* Play()：播放序列。
* Restart()：重新启动序列的播放。
* Rewind()：将序列倒回到第一个Tween对象并暂停。
* Kill()：立即停止序列的播放并销毁所有Tween对象。