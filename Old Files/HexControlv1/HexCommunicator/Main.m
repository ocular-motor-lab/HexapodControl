clear;clc;
%%% Parse Received Commands
%%% Function to send command on timer, instead of building a matrix first
%%% Plot Time, see how it looks
%%% Find out queue of machine
%%% Use Debug log, from Notion!
%%% Test timing on like 10 commands that you can perceive
asm = NET.addAssembly('C:\Users\omlab-admin\Desktop\HexapodControlv2.dll');%'C:\Users\omlab-admin\Documents\GitHub\HexapodControl\HexControl\HexCommunicator\HexCommunicator.dll');
global hexControl;
hexControl = HexapodControlv2.Class1;
global valueArray;
valueArray = zeros(3,1); %[ Roll, Yaw, Pitch ]
pushbuttonPlot;
% hexControl.Connect();

%% Timer Functions
global move_array;

function pushbuttonPlot
fig = uifigure;
fig.Position = [100 100 700 400];

cbxR = uicheckbox(fig);
cbxR.Text = 'PSI - Roll';
cbxR.Position = [50 350 90 25];

cbxY = uicheckbox(fig);
cbxY.Text = 'THETA - Yaw';
cbxY.Position = [50 320 90 25];

cbxP = uicheckbox(fig);
cbxP.Text = 'PHI - Pitch';
cbxP.Position = [50 290 90 25];

buildB = uibutton(fig);
buildB.Text = 'Build';
buildB.Position = [50 250 85 25];
buildB.Enable = false;

senB = uibutton(fig);
senB.Text = 'Send';
senB.ButtonPushedFcn = @sendCommands;
senB.Position = [450 250 85 25];
senB.Enable = false;

resetB = uibutton(fig);
resetB.Text = 'Reset';
resetB.ButtonPushedFcn = @resetCommand;
resetB.Position = [150 250 85 25];
resetB.Enable = false;

connectB = uibutton(fig);
connectB.Text = 'Connect';
connectB.ButtonPushedFcn = @connectChair;
connectB.Position = [250 250 85 25];

disconnectB = uibutton(fig);
disconnectB.Text = 'Disconnect';
disconnectB.ButtonPushedFcn = @disconnectChair;
disconnectB.Position = [350 250 85 25];
disconnectB.Enable = false;

deg = uislider(fig);
deg.Position = [150 370 300 3];
deg.Limits = [0 15];
deg.Value = 4;

degText = uitextarea(fig);
degText.Position = [470 350 65 25];
degText.Value = num2str(deg.Value);

freq = uislider(fig);
freq.Position = [150 320 300 3];
freq.Limits = [0 2];
freq.Value = 1;

freqText = uitextarea(fig);
freqText.Position = [470 300 65 25];
freqText.Value = num2str(freq.Value);

deg.ValueChangedFcn = @(deg, event) updateFreq(deg, freq, degText, freqText);
degText.ValueChangedFcn = @(degText, event) updateFreqT(deg, freq, degText, freqText);
freq.ValueChangedFcn = @(freq, event) updateDeg(deg, freq, degText, freqText);
freqText.ValueChangedFcn = @(freqText, event) updateDegT(deg, freq, degText, freqText);

buildB.ButtonPushedFcn   = @(buildB, event) BuildCommandPushed(deg, freq, cbxR, cbxY, cbxP);
connectB.ButtonPushedFcn = @(connectB, event) connectChair(senB, resetB, connectB, buildB, disconnectB);
disconnectB.ButtonPushedFcn = @(disconnectB, event) disconnectChair(senB, resetB, connectB, buildB, disconnectB);

cbxR.ValueChangedFcn = @(cbxR, event) RollSelected(cbxP, cbxY);
cbxY.ValueChangedFcn = @(cbxY, event) YawSelected(cbxR, cbxP);
cbxP.ValueChangedFcn = @(cbxP, event) PitchSelected(cbxR, cbxY);

    function RollSelected(P, Y)
        P.Value = 0;
        Y.Value = 0;
        global valueArray;
        valueArray = [1, 0, 0];
    end

    function YawSelected(R, P)
        R.Value = 0;
        P.Value = 0;
        global valueArray;
        valueArray = [0, 1, 0];
    end

    function PitchSelected(R, Y)
        R.Value = 0;
        Y.Value = 0;
        global valueArray;
        valueArray = [0, 0, 1];
    end

    function BuildCommandPushed(deg, freq, psi, theta, phi)
        global move_array;
        move_array = create_move_array(10, deg.Value, freq.Value, double(psi.Value), double(theta.Value), double(phi.Value));
        length(move_array);
    end

    function updateDeg(deg, freq, dText, fText)
        %%% TEST AREA %%%
        global valueArray;
        if valueArray(3)
            try
                deg.Value = 1.3/freq.Value + 0.015;
                warning('Equation');
            catch
                warning('Out of Bounds');
                deg.Value = 15;
                freq.Value = 1.3/(deg.Value - 0.015);
            end
        else %%% END TEST AREA %%%
            try
                deg.Value = 1.4/freq.Value - 0.05;
            catch
                warning('Out of Bounds');
                deg.Value = 15;
                freq.Value = 1.4/(deg.Value + 0.05);
            end
        end %%% <- Part of Test!
        dText.Value = num2str(deg.Value);
        fText.Value = num2str(freq.Value);
    end

    function updateFreq(deg, freq, dText, fText)
        try
            freq.Value = 1.4/(deg.Value + 0.05);
        catch
            warning('Out of Bounds');
            freq.Value = 2;
            deg.Value = 1.4/freq.Value - 0.05;
        end
        
        dText.Value = num2str(deg.Value);
        fText.Value = num2str(freq.Value);
    end

    function updateDegT(deg, freq, dText, fText)
        f_a = str2double(fText.Value);
        if f_a > 2
            f_a = 2;
        elseif f_a < 0
            f_a = 0;
        end
        
        try
            freq.Value = f_a;
            deg.Value = 1.4/f_a - 0.05;
        catch
            warning('Out of Bounds');
            deg.Value = 15;
            freq.Value = 1.4/(deg.Value + 0.05);
        end
        
        dText.Value = num2str(deg.Value);
        fText.Value = num2str(freq.Value);

    end

    function updateFreqT(deg, freq, dText, fText)
        f_a = str2double(dText.Value);
        if f_a > 15
            f_a = 15;
        elseif f_a < 0
            f_a = 0;
        end
        
        try
            deg.Value = f_a;
            freq.Value = 1.4/(f_a + 0.05);
        catch
            warning('Out of Bounds');
            freq.Value = 2;
            deg.Value = 1.4/freq.Value - 0.05;
        end
        
        dText.Value = num2str(deg.Value);
        fText.Value = num2str(freq.Value);
        
    end

    function sendCommands(src,event)
        global move_array;
        global hexControl;
        ti = timer('ExecutionMode','fixedRate','Period',0.01,'TasksToExecute',length(move_array));
        
        ti.UserData.counter = 1;
        ti.UserData.mvmt = move_array;
        ti.UserData.connection = hexControl;
        ti.UserData.connection.userName = "omlab-admin";

        ti.TimerFcn = {@theCallbackFunction};
        
        ti.StopFcn = @(~,thisEvent)delete(ti);

        start(ti);
        
        function theCallbackFunction(src, event)
    
            positions = src.UserData.counter;
            cmd = src.UserData.mvmt(positions,:);
            src.UserData.counter = positions + 1;
            src.UserData.connection.SendCommand(cmd(3), cmd(4), cmd(5),...
                                                cmd(6), cmd(1), cmd(2));

        end
    end

    function connectChair(senB, resetB, connectB, buildB, disconnectB)
        global hexControl;
        hexControl.Connect(strcat('input_log_', string(datestr(now,'HH:MM:SS')).replace(':', '-'), '.csv'));
        senB.Enable = true;
        resetB.Enable = true;
        buildB.Enable = true;
        disconnectB.Enable = true;
        connectB.Enable = false;
    end

    function disconnectChair(senB, resetB, connectB, buildB, disconnectB)
        global hexControl;
        hexControl.Disconnect();
        senB.Enable = false;
        resetB.Enable = false;
        buildB.Enable = false;
        disconnectB.Enable = false;
        connectB.Enable = true;
    end

    function resetCommand(src, event)
        global move_array
        global hexControl;
        move_array = [];
        hexControl.ResetChair();
        %src.UserData.connection.SendCommand(0, 0, 0, 0, 0, 0);%
    end

end

