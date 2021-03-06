clear;clc;
asm = NET.addAssembly('C:\Users\omlab-admin\Documents\GitHub\HexapodControl\HexControl\HexCommunicator\HexCommunicator.dll');
%global hexControl;
hexControl = WindowsFormsApp1.Form1;
% global valueArray;
% valueArray = zeros(3,1); %[ Roll, Yaw, Pitch ]
pushbuttonPlot(hexControl);

%% Timer Functions

function pushbuttonPlot(hCont)
fig = uifigure;
fig.Position = [100 100 700 400];

setappdata(fig, 'checkboxValues', [0,0,0]);

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

senB = uibutton(fig);
senB.Text = 'Send';
senB.ButtonPushedFcn = @(senB, event) sendCommands(hCont);
senB.Position = [390 250 85 25];

resetB = uibutton(fig);
resetB.Text = 'Reset';
resetB.ButtonPushedFcn = @(resetB, event) resetCommand(hCont);
resetB.Position = [150 250 85 25];

deg = uislider(fig);
deg.Position = [200 370 200 3];
deg.Limits = [0 15];
deg.Value = 4;

degText = uitextarea(fig);
degText.Position = [410 350 65 25];
degText.Value = num2str(deg.Value);

freq = uislider(fig);
freq.Position = [200 320 200 3];
freq.Limits = [0 2];
freq.Value = 1;

freqText = uitextarea(fig);
freqText.Position = [410 300 65 25];
freqText.Value = num2str(freq.Value);

deg.ValueChangedFcn      = @(deg, event) UpdateValues(deg, freq, degText, freqText,     [1,0,0,0]);
freq.ValueChangedFcn     = @(freq, event) UpdateValues(deg, freq, degText, freqText,    [0,1,0,0]);
degText.ValueChangedFcn  = @(degText, event) UpdateValues(deg, freq, degText, freqText, [0,0,1,0]);
freqText.ValueChangedFcn = @(freqText, event) UpdateValues(deg, freq, degText, freqText,[0,0,0,1]);

buildB.ButtonPushedFcn = @(buildB, event) BuildCommandPushed(deg, freq, cbxR, cbxY, cbxP);

cbxR.ValueChangedFcn = @(cbxR, event) AxisSelected(cbxR, cbxY, cbxP, [1, 0, 0]);
cbxY.ValueChangedFcn = @(cbxY, event) AxisSelected(cbxR, cbxY, cbxP, [0, 1, 0]);
cbxP.ValueChangedFcn = @(cbxP, event) AxisSelected(cbxR, cbxY, cbxP, [0, 0, 1]);

    function AxisSelected(R, Y, P, vals)
        R.Value = vals(1);
        Y.Value = vals(2);
        P.Value = vals(3);
        setappdata(fig, 'checkboxValues', vals);
%         global valueArray;
%         valueArray = vals;
    end

    function BuildCommandPushed(deg, freq, psi, theta, phi)
        setappdata(fig, 'move_array', create_move_array(10, deg.Value, freq.Value, double(psi.Value), double(theta.Value), double(phi.Value)));
    end

    function UpdateValues(deg, freq, degText, freqText, selection)
        vals = getappdata(fig, 'checkboxValues');
        if vals(3)
            num = 1.3;
            adjuster = 0.015;
        else
            num = 1.4;
            adjuster = 0.05;
        end
        
        if selection(1)
            degr = deg.Value;
            DorF = 0;
        elseif selection(2)
            fre = freq.Value;
            DorF = 1;
        elseif selection(3)
            degr = str2double(degText.Value);
            DorF = 0;
        elseif selection(4)
            fre = str2double(freqText.Value);
            DorF = 1;
        end
        
        if DorF
            degr = calculateForD(0, num, fre, adjuster);
        else
            fre = calculateForD(1, num, degr, adjuster);
        end
        
        deg.Value = degr;
        freq.Value = fre;
        degText.Value = num2str(degr);
        freqText.Value = num2str(fre);

    end

    function out = calculateForD( ford, num, val, adjuster )
        if ford
            out = num/(val + adjuster);
        else
            out = num/val + adjuster;
        end
    end

    function sendCommands(hexControl)
        move_array = getappdata(fig, 'move_array');
        ti = timer('ExecutionMode','fixedRate','Period',0.01,'TasksToExecute',length(move_array));
        
        ti.UserData.counter = 1;
        ti.UserData.mvmt = move_array;
        ti.UserData.connection = hexControl;
        ti.UserData.connection.userName = "omlab-admin";

        ti.UserData.connection.Connect(strcat('input_log_', string(datestr(now,'HH:MM:SS')).replace(':', '-'), '.csv'));

        ti.TimerFcn = {@theCallbackFunction};
        
        %ti.StopFcn = @(~,thisEvent)delete(ti);

        start(ti);
        
        function theCallbackFunction(src, event)
    
            positions = src.UserData.counter;
            cmd = src.UserData.mvmt(positions,:);
            src.UserData.counter = positions + 1;
            %print(cmd);
            src.UserData.connection.SendCommand(cmd(3), cmd(4), cmd(5),...
                                                cmd(6), cmd(1), cmd(2));

        end
    end

    function resetCommand(hexControl)
        t1 = timerfind;
        delete(t1);
        hexControl.SendCommand(0, 0, 0, 0, 0, 0);
    end

end

