   M S B A M L   `   `   `  ����A  =Cinema, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CinemaCinema  5          Cinema.Account   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:Cinema     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  Account��5      $	��800��5	      $	��600��5
       � AllowsTransparency .   � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5      .��5      � .+�5   
   � -   5         �Name$
 Head��5         WindowHead_style#��[  6   .+�5      �� -   5         ��Name$ usersActions��5      .+�5      �� -   5         ��Name$ HandlerRectangle��5        HandlerRect_style#��[ 5      �� 5      -   5         ��Name$
 Logo��5        logosPlaceBorder_style#��[ 6'   ._�5      �� $	��130��5      $	��130��5      $��Images/cinemaLogo.png��5      5      �� 5      -   5         ��Name$ CinemaCompanyName��5        TitleText_style#��[ 5      $��	Cinematik��5       �� 5!      -   5        	 ��Name$	 userRegistrationButton��5"      $��Регистрация��5$        HeadButtons_style#��[ 5%      �� 5&      -   5        $	 userAuthorizationButton��5'      $��Аккаунт��5(      #��[ 5)      5*      �� 5+      $��Bottom=�6   .+�5,      �� -   5        $	 GoBackToMainWindow��5-      $��Вернуться��5/      #��[ 50      �� 51      -	   5        $	 closeApplication��52      $��Закрыть��54        CloseAppButtons_style#��[ 55      56      59      � 5:   
   -
   5        $
 Body��5:        WindowBody_style#��[ 6   .+�5;      �� -   5        $+��%Область_регистрации��5;       ) %registration_authorizationPlace_style#��[ 60   ._�5<      � .+�5>      �� .+�5@      ��   textInstruction_style#��[ 6$   $��
Логин��6S   �� 5A      -   5        
 ��Name$
 CustomerLogin��5B       !	 CustomerInformationText_style#��[	 5C      $��SomeText��5D      �� 5G      #��[ 6$   $��
Почта��6S   �� 5H      -   5        $
 CustomerEmail��5I      #��[	 5J      $��SomeText��5K      �� 5N      #��[ 6$   $��Пароль��6S   �� 5O      -   5        $
 CustomerPassword��5P      #��[	 5Q      $��SomeText��5R      �� 5U      #��[ 6$   $��Возраст��6S   �� 5V      -   5        $
 CustomerAge��5W      #��[	 5X      $��SomeText��5Y      5Z      5[      5]      �� 5^      -   5        $	 ReLoginButton��5_      $��
Выйти��5a       )
 %registration_autorizationButton_style#��[
 5b      5c      5d      5e      