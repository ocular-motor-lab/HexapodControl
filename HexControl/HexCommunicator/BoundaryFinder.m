DegRot = [0.2:0.2:15];
Freeqs = [0.01:0.01:1.5];

% Length of Time
Duration = 60; % in sec

% Rotation Reference in cm (?); Z is up and down
RefX = 0; RefY = 0; RefZ = 18;

% Movement axis
X = 0; Y = 0; Z = 89;

% Axis of Rotation
phi = 0; % Roll? 
theta = 0; % Yaw
psi = 0; % Pitch?

% Horizontal Movement (Z)
Begin = 89;
Ending = 110;

% Axis of Rotation (Z)
EyeReference = 60;

% Degrees of Rotation
RotDeg = 12;
elements = 0:0.01:Duration;
Amp = RotDeg;
Shift = 0;
Phase = pi;

Freq = 0.05;
Period = 1/Freq; % Time to complete a full cycle

Res = Amp*sin(elements*2*pi/Period + Phase) + Shift;
RotMovement = Res;

counter2 = 1;
%%%%%%%%%%%%%%%%
for j = Freeqs
    for k = DegRot
        Amp = k;
        Freq = j;
        Period = 1/Freq;
        Res = Amp*sin(elements*2*pi/Period + Phase) + Shift;
        RotMovement = Res;

        counter = 1;

        [tempStorage,validL,~,~,~,~,~] = CalculateArmLength(X, Y, Ending,...
                                                 RotMovement(1), theta, psi,...
                                                 RefX, RefY, RefZ);

        for i = RotMovement
            [ArmExtensionMMs,validL,~,~,~,~,~] = CalculateArmLength(X, Y, Ending,...
                                                 i, theta, psi,... % phi, theta-yaw, psi-roll (i)
                                                 RefX, RefY, RefZ);
            if sum((ArmExtensionMMs - tempStorage) >= [1.3 1.3 1.3 1.3 1.3 1.3]) >= 1
                validL = false;
            end
            if validL
                tempStorage = ArmExtensionMMs;
                counter = counter + 1;
                if counter == length(RotMovement)
                    goodOnes(counter2,:) = [j k];
                    counter2 = counter2 + 1;
                end
            else
                break
            end

        end
    end
end


%%%%%%%%%%%%%%%%%

% Reference Function Call
% [ArmExtensionMMs,validL,~,~,~,~,~] = CalculateArmLength(X, Y, Z,...
%                                                         phi, theta, psi,...
%                                                         RefX, RefY, RefZ)
% isLengthValid,restingArmLengths, currentArmLengths, Rwp, biw, ai
