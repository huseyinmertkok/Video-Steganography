<h1 align="center">Video-Steganography</h1>
•This code allows user to hide text with up to 65.535 letters (including spaces).<br />
•The code also allows user to reveal the text from videos.<br />
•The G and B values of the last pixel of the first frame holds the lenght of the text. It's calculated as;<br />
•B.value + G.value * 256;
<br />
<br />

<h2>Used Dependencies</h2>
•Accord.Video.FFMPEG<br />
•FFMpegSharp<br />
•FFMpegSharp.FFMPEG<br />
•AviFile<br />
•NAudio<br />

<h2>UI</h2>
• The first screen when user runs the code.<br />
<p align="center">
  <img src="./photos/firstLook.png" title="UI">
</p>

<h2>During Hide Process</h2>
<p align="center">
  <img src="./photos/afterHide.png" title="UI2">
</p>

<h2>After Unhide</h2>
<p align="center">
  <img src="./photos/afterUnhide.png" title="UI3">
</p>
