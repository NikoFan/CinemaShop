   M S B A M L   `   `   `  ����A  =Cinema, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema  5          Cinema.RegistrationWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:Cinema     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  RegistrationWindow��5      $	��800��5	      $	��600��5
       � AllowsTransparency .   � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5      .��5      � .+�5   
   � -   5         �Name$
 Head��5         WindowHead_style#��[  6   .+�5      �� -   5         ��Name$ usersActions��5      .+�5      �� -   5         ��Name$ HandlerRectangle��5        HandlerRect_style#��[ 5      �� 5      -   5         ��Name$
 Logo��5        logosPlaceBorder_style#��[ 6'   ._�5      �� $	��130��5      $	��130��5      $��Images/cinemaLogo.png��5      5      �� 5      -   5         ��Name$ CinemaCompanyName��5        TitleText_style#��[ 5      $��	Cinematik��5       �� 5!      -   5        	 ��Name$	 userRegistrationButton��5"      $��Регистрация��5#        HeadButtons_style#��[ 5$      �� 5%      -   5        $	 userAuthorizationButton��5&      $��Аккаунт��5(      #��[ 5)      5*      �� 5+      $��Bottom=�6   .+�5,      �� -   5        $	 GoBackToMainWindow��5-      $��Вернуться��5/      #��[ 50      �� 51      -	   5        $	 closeApplication��52      $��Закрыть��54        CloseAppButtons_style#��[ 55      56      59      � 5:   
   -
   5        $
 Body��5:        WindowBody_style#��[ 6   .+�5;      �� -   5        $+��%Область_регистрации��5;       ) %registration_authorizationPlace_style#��[ 60   ._�5<      � .+�5>      �� .+�5@      ��   textInstruction_style#��[ 6$   $��
Логин��6S   �� 5A      -   5        
 ��Name$
 customer_login��5B       �� 	MaxLength$ 50��5C       	 textInputPlace_style#��[	 5D      �� 5E       
 underLine_style#��[
 6   �� 5H      #��[ 6$   $��
Почта��6S   �� 5I      -   5        $
 customer_email��5J      $ 50��5K      #��[	 5L      �� 5M      #��[
 6   �� 5P      #��[ 6$   $��Пароль��6S   �� 5Q      -   5        $
 customer_password��5R      $ 20��5S      #��[	 5T      �� 5U      #��[
 6   �� 5X      #��[ 6$   $��Возраст��6S   �� 5Y      -   5        $
 customer_age��5Z      $ 3��5[      #��[	 5\      �� 5]      #��[
 6   5^      5_      5a      �� 5b      -   5        $	 RegistrationButton��5c      $��Регистрация��5e       ) %registration_autorizationButton_style#��[ 5f      5g      5h      5i      