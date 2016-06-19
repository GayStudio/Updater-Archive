# Written by Kenvix @ 2016/5/14
# Variables
xpath="/home/mc" #Minecraft Path
jarfile="PaperSpigot-latest.jar" #Minecraft Jar File
screen="mc" #Name of screen
#comm="-Dfml.queryResult=confirm -Xms3400M -Xmx4500M -XX:+AggressiveOpts -XX:+UseCompressedOops" #Forge Command
comm="-Xmx280M -XX:+AggressiveOpts " #Command
sleeptime="3s" #Loop mode Sleep time
xpid="/run/moecraft.pid" #Moecraft Server Console(Loop mode) PID File
mcsign="#Controlled-By-X" #Moecraft Server Argument Sign
# Core
cd ${xpath}
echo -e "\033[34;49;1m(C) 2016 MoeCraft All Rights Reserved\e[0m"
echo -e "\033[32;49;1mMoeCraft Server Console\e[0m // Written by Kenvix @ 2016/5/15"
echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
case $1 in
	r)
        ps -fe | grep ${mcsign} | grep -v grep
        if [ $? -eq 0 ]; then
            echo -e "\033[32;1mError: Minecraft has already running!\e[0m"
            echo "Stop it by using 'x c', Kill it by using 'x k'"
        else
            echo -e "\033[32;1mRunning Server ...\e[0m"
            echo -e "Using Jar File: ${jarfile}"
            echo -e ${comm}
            echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
            java ${comm} -jar ${xpath}/${jarfile} ${mcsign}
            exitcode=$?
            echo ""
            echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
            echo -e "\033[32;1mExit Code: ${exitcode}\e[0m"
            echo -e "\033[34;49;1mMoeCraft Server Closed\e[0m"
        fi
    ;;
    l)
        if [ -f "$xpid" ]; then
            echo -e "\033[32;1mError: Minecraft Server Console + Loop Mode has already running!\e[0m"
            echo "Stop it by using 'x c', Kill it by using 'x k'"
        else
            ps -fe | grep ${mcsign} | grep -v grep
            if [ $? -eq 0 ]; then
                echo -e "\033[32;1mError: Minecraft has already running!\e[0m"
                echo "Stop it by using 'x c', Kill it by using 'x k'"
            else
                echo -e "\033[32;1mRunning Server ... With loop mode!\e[0m"
                echo -e "PID File: [$$] ${xpid}"
                echo -e "Using Jar File: ${jarfile}"
                echo -e ${comm}
                echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
                touch ${xpid}
                echo $$ > ${xpid}
                trap "echo -e ' \033[31;40;1m Ctrl+C: SIGINT! Stopping Loop Mode...\e[0m' & rm -rf ${xpid} & exit 0" 2
                trap "echo -e ' \033[31;40;1m Kill: SIGTERM! Stopping Loop Mode...\e[0m' & rm -rf ${xpid} & exit 0" 2
                while [[ true ]];do
                    java ${comm} -jar ${xpath}/${jarfile} ${mcsign}
                    exitcode=$?
                    echo ""
                    echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
                    echo -e "\033[32;1mExit Code: ${exitcode}\e[0m"
                    echo -e "\033[34;49;1mMoeCraft Server Closed\e[0m Run again after ${sleeptime}"
                    echo "Stpped at " `date '+%Y-%m-%d %H:%M:%S'` " press Ctrl+C to cancel reboot"
                    sleep ${sleeptime}
                    echo "Running Server Again ..."
                    echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
                done;
            fi
        fi
    ;;
    c)
        echo -e "\033[32;1mStopping Server ...\e[0m"
        if [ -f "$xpid" ]; then
            echo "PID File exists, Kill Minecraft Server Console + Loop Mode and Delete!"
            sudo kill -s KILL `cat ${xpid}`
            rm -rf ${xpid}
        fi
        pid=`ps -x | grep ${mcsign} | grep -v grep`
        if [ $? -ne 0 ]; then
            echo -e "\033[32;1mError: Process Not running!\e[0m"
        else
            sudo kill -s INT `echo $pid | awk '{ print $1 }'`
            echo -e "\033[32;1mStopping!\e[0m"
            echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
            free -hb
        fi
    ;;
    k)
        echo -e "\033[32;1mKilling Server ...\e[0m"
        if [ -f "$xpid" ]; then
            echo "PID File exists, Kill Minecraft Server Console + Loop Mode and Delete!"
            sudo kill -s KILL `cat ${xpid}`
            rm -rf ${xpid}
        fi
        pid=`ps -x | grep ${mcsign} | grep -v grep`
        if [ $? -ne 0 ]; then
            echo -e "\033[32;1mError: Process Not running!"
        else
            sudo kill -s KILL `echo $pid | awk '{ print $1 }'`
            echo -e "\033[32;1mKilled!"
            echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
            free -h
        fi
    ;;
    x)
        sudo screen -d ${screen}
        sleep 0.3s
        sudo screen -r ${screen}
    ;;
    g)
        sudo screen -x ${screen}
    ;;
    sc)
        sudo screen -S ${screen} -t MoeCraft-Server-Console----mc
        cd /home/mc
    ;;
    sd)
        sudo screen -d ${screen}
    ;;
    sk)
        sudo screen -X kill
    ;;
    *)
        echo -e "\033[31;40;1m Usage: Arguments:\e[0m"
        echo "r    Run the minecraft"
        echo "l    Run the minecraft with loop mode. (You can stop it by 'x c')"
        echo "c    Close the server like /stop command"
        echo "k    FORCE Kill minecraft(java) process"
        echo "x    Exclusive Reattach to the minecraft screen and detach the elsewhere running screen"
        echo "g    Reattach to the minecraft screen which is not detached"
        echo "sc   Create a screen named ${screen} . You can kill the screen with Ctrl+A K"
        echo "sd   Detach a screen named ${screen}"
        echo "sk   Kill a screen named ${screen}"
    ;;
esac
echo -e "\033[31;49;1m------------------------------------------------------------\e[0m"
