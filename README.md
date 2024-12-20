# Dicom-Viewer 

I developed this when I had just started my full-time career and when I was getting acquainted with C#, .NET, WPF, and WinForms. New tech stack for me. This was a task given to me to learn and understand DICOM files and how to work with them

So, it's a DICOM Viewer Application which allows you to view and manage images stored in the DICOM format. There are many DICOM Viewer Applications available on internet and used by many medical and other professionals.

Some features that I was able to implement: -
- View image in Both Monochrome and RGB format
- View all the tags within that DICOM file and nested tags as well in a tabular readable format
- Window Width and Window Center Sliders to adjust contrast and brightness, so that you can easily segregate bones, tissues and fluids using the sliders. You can see the same in the Video, how on sliding the Image is getting changed.
- Multi DICOM Files support with buttons to navigate.

I also implemented the Modality Transformation and Volume Transformation (Windowing) algorithms, with Color Adjustments for RGB. The toughest part was to Process the Tags and the Nested Tags within those tags and then creating a separate window for the nested tags table as well. This application can access nested tags till 1st level as getting nested tags till the last level required some extra DSA skills 😅 and TC of O(N^3). But I have already figured out a solution which I will integrate it with additional new features that I have in my mind.

In future, I'll be trying to implement MVVM pattern and allow CRUD operations on DICOM Contents.

#### [LinkedIn Post](https://www.linkedin.com/posts/adarsh-gupta-1086351a0_hi-all-i-have-created-a-lot-of-things-but-activity-7190079640219090944-hJ2a?utm_source=share&utm_medium=member_desktop) with demo video of working application
