:SCHTASKS /Create /F /SC ONSTART /TN Stslck /TR D:\!\Stslck.exe /RL HIGHEST
SCHTASKS /Query /TN Stslck /XML > Stslck.xml
:SCHTASKS /Create /F /TN Stslck /XML D:\!\Stslck1.xml
:SCHTASKS /run /tn stslck
:SCHTASKS /Delete /F /TN Stslck2
:SCHTASKS /Change /? > sss