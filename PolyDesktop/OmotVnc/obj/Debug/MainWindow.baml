   M S B A M L   `   `   `  ����K  GOmotVnc.Controls, Version=1.0.0.5, Culture=neutral, PublicKeyToken=nullcCclr-namespace:PollRobots.OmotVnc.Controls;assembly=OmotVnc.ControlsPollRobots.OmotVnc.Controls  5   	    OmotVnc   OmotVnc.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   XControlsCclr-namespace:PollRobots.OmotVnc.Controls;assembly=OmotVnc.Controls     5   	     � Title$  Old Man of the VNC��5   	    � WindowStyle$ ThreeDBorderWindow=�5   	    � 	Resources 5	       �� 
TargetType �� Property �� Value �� Triggers �� Binding �� Value��%�   '��       �� 5
   
   " ��6   .�5      ��  �@J�Opacity$ 1Q�6(    5      �� 5       6   �� *IsConnected+	 False6>   .a�5      ��  �@J�Opacity$	 0.5Q�60   5      5      5      .��5      ]� .+�5   
   o� -   5        $��MainMenu��5      $	��Top=�6   .@�5      m� $
��File��6   .@�5      m� $��
Connect...��6   	 m� Command	 6/   �� *ConnectCommand+m� 5      $��
Disconnect��6   	 6/   �� *DisconnectCommand+�� 5      m� 5      $
��Exit��6   	 6)   �� *ExitCommand+5      m� 5      $��Display��6   .@�5      m� $
��Zoom��6   .@�5      m� $	��50%��6   
 m� CommandParameter$
 50��6P   	 6,   �� *SetScaleCommand+6P   m� 5      $	��75%��6   $
 75��6P   	 6,   �� *SetScaleCommand+6P   m� 5       $
��100%��6   $	
 100��6Q   	 6-   �� *SetScaleCommand+6Q   m� 5!      $
��150%��6   $	
 150��6Q   	 6-   �� *SetScaleCommand+6Q   m� 5"      $
��200%��6   $	
 200��6Q   	 6-   �� *SetScaleCommand+6Q   �� 5#      m� 5$      $��Fit in window��6   	 66   �� *SetScaleToFitCommand+5%      m� 5&      $��Refresh��6   	 6,   �� *RefreshCommand+m� 5'      $��Toggle local cursor��6   	 68   �� *ToggleLocalCursorCommand+5(      5)      )   #PollRobots.OmotVnc.Controls.VncHost  5+   
   -   5          Name$ VncHost��5+         Scale 6,   �� *Scale+   
ScaleToFit 6D   �� *
ScaleToFit+5,      5.      