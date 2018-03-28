## CODEPLEX Archive Extractor

This is a small F# application to help extracting the files from [CodePlex archives](https://archive.codeplex.com/) with their original names.

I created this application to help with restoring old DNN Platform releases from CodePlex after it got retired and to create the new Archived files. I blogged about this here: http://www.dnnsoftware.com/community-blog/cid/155497/dnn-releases-archives
And David Poindexter completed the task and blogged about it here: http://www.dnnsoftware.com/community-blog/cid/155512/dnn-archive-releases-oss-and-answering-the-call


To use the application, you need to have Visual Studio with F# components installed. Then you need to do the following:
- Download and extract the CodePlex archive in a folder at the same level where you clone this repository. Call this folder "CodePlexReleases"
- Compile and run the application from the DEBUG/Release folder
- The application will create a folder at the same level of the CodePlex extracted folder called "ExtractedReleases" and will move the files to it with their original name maintaining folders structure.

Enjoy!
