function [allMovement] = psi_roll(hexControl)
%PHI Summary of this function goes here
%   Detailed explanation goes here



% Length of Time
Duration = 60; % in sec

% Rotation Reference in cm (?); Z is up and down
RefX = 0;
RefY = 0;
RefZ = 18;

% Movement axis
X = 0;
Y = 0;
Z = 89;

% Axis of Rotation
phi = 0; % Roll?
theta = 0; % Yaw
psi = 0; % Pitch?

Begin = 89;
Ending = 110;

InitialMovement = Begin:0.1:Ending;

% Axis of Rotation (Z)
EyeReference = 60;

% Degrees of Rotation (psi)
RotDeg = 6;

elements = 0:0.01:Duration;
Amp = RotDeg;
Shift = 0;
Phase = pi;

% Torsion Velocity
tVel = 60; % Degrees/sec
Freq = 15; % Time to complete a full cycle (MAX: 8.27deg/s)
% i am a fool, for the true limit is in the mm/ms velocity of 1.3mm/10ms

Res = Amp*sin(elements*2*pi/Freq + Phase) + Shift;
RotMovement = Res;


counter = 1;

cmdInvalid = false;

for i=InitialMovement
    [ArmExtensionMMs,validL,~,~,~,~,~] = CalculateArmLength(X, Y, i,...
                                         phi, theta, psi,...
                                         RefX, RefY, RefZ);
    if validL
%         cmdInvalid = hexControl.SendCommand(ArmExtensionMMs(1),...
%                                       ArmExtensionMMs(2),...
%                                       ArmExtensionMMs(3),...
%                                       ArmExtensionMMs(4),...
%                                       ArmExtensionMMs(5),...
%                                       ArmExtensionMMs(6));
    else
        validL
        hexControl.SendCommand(double(0),double(0),double(0),double(0),double(0),double(0))
        break
    end

    if ~cmdInvalid
        allMovement(counter,:) = ArmExtensionMMs;
        counter = counter + 1;
    end
end

for i = RotMovement
    [ArmExtensionMMs,validL,~,~,~,~,~] = CalculateArmLength(X, Y, Ending,...
                                         phi, theta, i,... % phi, theta-yaw, psi-roll (i)
                                         RefX, RefY, RefZ);
    if validL
%         cmdInvalid = hexControl.SendCommand(ArmExtensionMMs(1),...
%                                       ArmExtensionMMs(2),...
%                                       ArmExtensionMMs(3),...
%                                       ArmExtensionMMs(4),...
%                                       ArmExtensionMMs(5),...
%                                       ArmExtensionMMs(6));
    else
        validL
        hexControl.SendCommand(double(0),double(0),double(0),double(0),double(0),double(0))
        break
    end

    if ~cmdInvalid
        allMovement(counter,:) = ArmExtensionMMs;
        counter = counter + 1;
    end
end

allMovement = double(allMovement);

if (max(max(diff(allMovement))) >= 1.3)
    disp('Not Good');
else
    disp('Good');
end

end

